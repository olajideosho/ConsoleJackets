using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleJackets.Models;
using ConsoleJackets.ViewModels;
using CoreLocation;
using Foundation;
using UIKit;

namespace ConsoleJackets.ViewControllers
{
    public partial class UploadJacketViewController : UIViewController, ICLLocationManagerDelegate
    {
        public CLLocationManager locationManager = new CLLocationManager();
        public CLGeocoder geocoder = new CLGeocoder();
        public string country = "";
        public UploadJacketViewModel uploadJacketViewModel;
        public UploadJacketViewController() : base("UploadJacketViewController", null)
        {
        }

        public UploadJacketViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            uploadJacketViewModel = new UploadJacketViewModel();

            uploadStatusLabel.TextColor = UIColor.LightGray;

            jacketOwnerTextField.Layer.CornerRadius = 10;
            jacketOwnerTextField.ClipsToBounds = true;
            jacketIdTextField.Layer.CornerRadius = 10;
            jacketIdTextField.ClipsToBounds = true;
            secretKeyTextField.Layer.CornerRadius = 10;
            secretKeyTextField.ClipsToBounds = true;

            uploadJacketView.Layer.CornerRadius = 10;
            uploadJacketView.ClipsToBounds = true;
            uploadJacketButton.Layer.CornerRadius = 10;
            uploadJacketButton.ClipsToBounds = true;
            uploadJacketButton.TouchUpInside += UploadJacketButton_TouchUpInside;

            xButton.TouchUpInside += XButton_TouchUpInside;

            uploadStatusLabel.Hidden = true;
        }

        private void XButton_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewController(true, () =>
            {
                LaunchViewController.ReloadDetails();
            });
        }

        private async void UploadJacketButton_TouchUpInside(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jacketOwnerTextField.Text) || string.IsNullOrEmpty(jacketIdTextField.Text) || string.IsNullOrEmpty(secretKeyTextField.Text))
            {
                uploadStatusLabel.TextColor = UIColor.FromRGB(143, 0, 0).ColorWithAlpha((nfloat)0.5);
                uploadStatusLabel.Text = "All fields are required";
                uploadStatusLabel.Hidden = false;
                return;
            }

            if(jacketIdTextField.Text.Length != 4)
            {
                uploadStatusLabel.TextColor = UIColor.FromRGB(143, 0, 0).ColorWithAlpha((nfloat)0.5);
                uploadStatusLabel.Text = "Jacket ID is 4 characters";
                uploadStatusLabel.Hidden = false;
                return;
            }

            var charMatch = Regex.Matches(jacketIdTextField.Text, @"[a-zA-Z]").Count;
            if (charMatch != 4)
            {
                uploadStatusLabel.TextColor = UIColor.FromRGB(143, 0, 0).ColorWithAlpha((nfloat)0.5);
                uploadStatusLabel.Text = "Jacket ID is from letters A to Z";
                uploadStatusLabel.Hidden = false;
                return;
            }

            locationManager = new CLLocationManager();

            locationManager.RequestAlwaysAuthorization();

            locationManager.RequestWhenInUseAuthorization();

            if (CLLocationManager.LocationServicesEnabled)
            {

                if(CLLocationManager.Status == CLAuthorizationStatus.Denied || CLLocationManager.Status == CLAuthorizationStatus.NotDetermined || CLLocationManager.Status == CLAuthorizationStatus.Restricted)
                {

                    var alert = UIAlertController.Create("Location Access Denied", "Please open settings and grant Console Jacket location permission", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Cancel, (alertAction) =>
                    {
                        DismissViewController(true, () =>
                        {
                            LaunchViewController.ReloadDetails();
                        });
                    }));
                    alert.AddAction(UIAlertAction.Create("Open Settings", UIAlertActionStyle.Default, async (alertAction) =>
                    {
                        var settingsUrl = new NSUrl(UIApplication.OpenSettingsUrlString);
                        if (UIApplication.SharedApplication.CanOpenUrl(settingsUrl))
                        {
                            UIApplication.SharedApplication.OpenUrl(settingsUrl);
                        }
                        else
                        {
                            uploadStatusLabel.TextColor = UIColor.FromRGB(143, 0, 0).ColorWithAlpha((nfloat)0.5);
                            uploadStatusLabel.Text = "Something went wrong";
                        }
                    }));
                    PresentViewController(alert, true, null);
                }
                else
                {
                    locationManager.Delegate = this;
                    locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
                    locationManager.RequestLocation();
                }

                
            }
            else
            {
                var alert = UIAlertController.Create("Location Error", "Location services not enabled on your device. Would you like to use 'Earth' as your location", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Cancel, (alertAction) =>
                {
                    DismissViewController(true, () =>
                    {
                        LaunchViewController.ReloadDetails();
                    });
                }));
                alert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, async (alertAction) =>
                {
                    country = "Earth";
                    await PerformPost();
                }));
                PresentViewController(alert, true, null);
            }
        }

        public async Task PerformPost()
        {
            uploadStatusLabel.TextColor = UIColor.White;
            uploadStatusLabel.Text = "Uploading...";
            uploadStatusLabel.Hidden = false;

            var jacket = new JacketUploadRequest
            {
                IndexId = 0,
                Owner = jacketOwnerTextField.Text,
                ID = jacketIdTextField.Text,
                Secret = secretKeyTextField.Text,
                Country = country
            };

            uploadJacketViewModel.payload = jacket;
            var response = await uploadJacketViewModel.UploadJacket();

            if (response.Error == true)
            {
                uploadStatusLabel.TextColor = UIColor.FromRGB(143, 0, 0).ColorWithAlpha((nfloat)0.5);
                uploadStatusLabel.Text = response.Message;
            }
            else if (response.Error == false)
            {
                uploadStatusLabel.TextColor = UIColor.FromRGB(0, 143, 0).ColorWithAlpha((nfloat)0.5);
                uploadStatusLabel.Text = response.Message;
                await Task.Delay(4000);

                DismissViewController(true, () =>
                {
                    LaunchViewController.ReloadDetails();
                });
            }
        }

        [Export("locationManager:didUpdateLocations:")]
        public void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            //throw new System.NotImplementedException();
            var currentLocation = locations.ToList().FirstOrDefault();

            geocoder.ReverseGeocodeLocation(currentLocation, async (placemarks, error) =>
            {
                var currentLocationPlacemark = placemarks.ToList().FirstOrDefault();
                if (!string.IsNullOrEmpty(currentLocationPlacemark.Country))
                {
                    country = currentLocationPlacemark.Country;
                    await PerformPost();
                }
                else
                {
                    var alert = UIAlertController.Create("Location Error", "We could not retrieve your location. Would you like to use 'Earth' as your location", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Cancel, (alertAction) =>
                    {
                        DismissViewController(true, () =>
                        {
                            LaunchViewController.ReloadDetails();
                        });
                    }));
                    alert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, (alertAction) =>
                    {
                        country = "Earth";
                    }));
                    PresentViewController(alert, true, null);
                }
                
            });
        }

        [Export("locationManager:didFailWithError:")]
        public void Failed(CLLocationManager manager, NSError error)
        {
            var alert = UIAlertController.Create("Location Error", "We could not retrieve your location. Please try again later", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Okay", UIAlertActionStyle.Cancel, (alertAction) =>
            {
                DismissViewController(true, () =>
                {
                    LaunchViewController.ReloadDetails();
                });
            }));
            PresentViewController(alert, true, null);
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

