using Foundation;
using System;
using UIKit;

namespace ConsoleJackets
{
    public partial class BuyJacketViewController : UIViewController
    {
        public BuyJacketViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            buyJacketView.Layer.CornerRadius = 10;
            buyJacketView.ClipsToBounds = true;
            doneButton.Layer.CornerRadius = 10;
            doneButton.TouchUpInside += DoneButton_TouchUpInside;
        }

        private void DoneButton_TouchUpInside(object sender, EventArgs e)
        {
            DismissModalViewController(true);
        }
    }
}