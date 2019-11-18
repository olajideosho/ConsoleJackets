using Foundation;
using System;
using UIKit;

namespace ConsoleJackets
{
    public partial class AboutViewController : UIViewController
    {
        public AboutViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            aboutView.Layer.CornerRadius = 10;
            aboutView.ClipsToBounds = true;

            doneButton.Layer.CornerRadius = 10;
            doneButton.TouchUpInside += DoneButton_TouchUpInside;
        }

        private void DoneButton_TouchUpInside(object sender, EventArgs e)
        {
            DismissModalViewController(true);
        }
    }
}