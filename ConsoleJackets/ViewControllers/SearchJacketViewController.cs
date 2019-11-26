using ConsoleJackets.ViewModels;
using Foundation;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UIKit;

namespace ConsoleJackets
{
    public partial class SearchJacketViewController : UIViewController
    {
        public static SearchJacketViewModel searchJacketViewModel;

        public SearchJacketViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            searchJacketViewModel = new SearchJacketViewModel();

            searchButton.SetTitle("Search", UIControlState.Normal);
            searchJacketView.Layer.CornerRadius = 10;
            searchJacketView.ClipsToBounds = true;

            jacketIdTextField.Layer.CornerRadius = 10;
            jacketIdTextField.ClipsToBounds = true;

            searchButton.Layer.CornerRadius = 10;
            searchButton.TouchUpInside += SearchButton_TouchUpInside;

            jacketOwnerLabel.Hidden = true;
            jacketIdLabel.Hidden = true;
            locationLabel.Hidden = true;

            doneButton.Layer.CornerRadius = 10;
            doneButton.ClipsToBounds = true;
            doneButton.TouchUpInside += DoneButton_TouchUpInside;
        }

        private async void SearchButton_TouchUpInside(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jacketIdTextField.Text))
            {
                return;
            }

            var charMatch = Regex.Matches(jacketIdTextField.Text, @"[a-zA-Z]").Count;
            if (charMatch != 4)
            {
                return;
            }

            if(jacketIdTextField.Text.Length != 4)
            {
                return;
            }

            jacketOwnerLabel.Hidden = true;
            jacketIdLabel.Hidden = true;
            locationLabel.Hidden = true;

            searchButton.UserInteractionEnabled = false;
            searchButton.SetTitle("Searching...", UIControlState.Normal);

            jacketIdTextField.Text = jacketIdTextField.Text.ToUpper();
            var jacket = await searchJacketViewModel.GetJacketWithId(jacketIdTextField.Text);
            if (jacket.JacketID == null)
            {
                jacketOwnerLabel.Text = "Jacket Owner: Not Found";
                jacketIdLabel.Text = "Jacket ID: Not Found";
                locationLabel.Text = "Location: Not Found";
            }
            else
            {
                jacketOwnerLabel.Text = $"Jacket Owner: {jacket.JacketOwner}";
                jacketIdLabel.Text = $"Jacket ID: /USR/BIN/{jacket.JacketID}";
                locationLabel.Text = $"Location: {jacket.Location}";
            }

            searchButton.SetTitle("Search Complete", UIControlState.Normal);
            jacketOwnerLabel.Hidden = false;
            await Task.Delay(1000);

            jacketIdLabel.Hidden = false;
            await Task.Delay(1000);

            locationLabel.Hidden = false;
            await Task.Delay(1000);

            jacketIdTextField.Text = "";
            searchButton.SetTitle("Search", UIControlState.Normal);
            searchButton.UserInteractionEnabled = true;

        }

        private void DoneButton_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewController(true, () =>
            {
                LaunchViewController.ReloadDetails();
            });
        }
    }
}