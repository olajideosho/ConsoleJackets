using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace ConsoleJackets
{
    public partial class SearchJacketViewController : UIViewController
    {
        public SearchJacketViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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
            jacketOwnerLabel.Hidden = true;
            jacketIdLabel.Hidden = true;
            locationLabel.Hidden = true;

            searchButton.UserInteractionEnabled = false;
            searchButton.SetTitle("Searching...", UIControlState.Normal);

            await Task.Delay(3000);

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
            DismissModalViewController(true);
        }
    }
}