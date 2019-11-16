using System;
using System.Threading.Tasks;
using UIKit;

namespace ConsoleJackets.ViewControllers
{
    public partial class UploadJacketViewController : UIViewController
    {
        UIColor successGreen = new UIColor(0, 0, 0, 0);
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
            successGreen = uploadStatusLabel.TextColor;

            uploadStatusLabel.TextColor = UIColor.White;

            uploadJacketView.Layer.CornerRadius = 10;
            uploadJacketView.ClipsToBounds = true;
            uploadJacketButton.Layer.CornerRadius = 4;
            uploadJacketButton.TouchUpInside += UploadJacketButton_TouchUpInside;

            uploadStatusLabel.Hidden = true;
        }

        private async void UploadJacketButton_TouchUpInside(object sender, EventArgs e)
        {
            uploadStatusLabel.Text = "Uploading...";
            uploadStatusLabel.Hidden = false;
            await Task.Delay(4000);

            uploadStatusLabel.TextColor = successGreen;
            uploadStatusLabel.Text = "Jacket Assigned Successfully";
            await Task.Delay(4000);
            DismissModalViewController(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

