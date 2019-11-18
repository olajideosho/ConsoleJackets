using System;
using System.Threading.Tasks;
using UIKit;

namespace ConsoleJackets.ViewControllers
{
    public partial class UploadJacketViewController : UIViewController
    {
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
            DismissModalViewController(true);
        }

        private async void UploadJacketButton_TouchUpInside(object sender, EventArgs e)
        {
            uploadStatusLabel.Text = "Uploading...";
            uploadStatusLabel.Hidden = false;
            await Task.Delay(4000);

            uploadStatusLabel.TextColor = UIColor.FromRGB(0, 143, 0).ColorWithAlpha((nfloat)0.5);
            uploadStatusLabel.Text = "Jacket Assigned Successfully";
            await Task.Delay(4000);
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

