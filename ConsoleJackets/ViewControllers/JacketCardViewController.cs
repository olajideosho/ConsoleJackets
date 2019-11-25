using Foundation;
using System;
using UIKit;

namespace ConsoleJackets
{
    public partial class JacketCardViewController : UIViewController
    {
        public UILabel JacketOwnerLabel = new UILabel();
        public UILabel JacketIDLabel = new UILabel();
        public UILabel LocationLabel = new UILabel();

        public JacketCardViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            JacketOwnerLabel = jacketOwnerLabel;
            JacketIDLabel = jacketIDLabel;
            LocationLabel = locationLabel;
            
            View.ClipsToBounds = true;
            View.Layer.CornerRadius = 10;
            View.LayoutIfNeeded();
        }
    }
}