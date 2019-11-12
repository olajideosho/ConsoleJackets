using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace ConsoleJackets
{
    public partial class LaunchViewController : UIViewController
    {
        public LaunchViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var logoVC = Storyboard.InstantiateViewController("ConsoleLogoAnimationViewController") as ConsoleLogoAnimationViewController;
            var consoleLogoView = logoVC.View;
            consoleLogoView.Frame = new CGRect(0, 0, 309, 48);
            logoView.AddSubview(consoleLogoView);
        }
    }
}