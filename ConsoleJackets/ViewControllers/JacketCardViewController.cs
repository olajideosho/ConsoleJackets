using Foundation;
using System;
using UIKit;

namespace ConsoleJackets
{
    public partial class JacketCardViewController : UIViewController
    {
        public JacketCardViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            
            View.ClipsToBounds = true;
            View.Layer.CornerRadius = 10;
            View.LayoutIfNeeded();
        }
    }
}