// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ConsoleJackets.ViewControllers
{
	[Register ("UploadJacketViewController")]
	partial class UploadJacketViewController
	{
		[Outlet]
		UIKit.UITextField jacketIdTextField { get; set; }

		[Outlet]
		UIKit.UITextField jacketOwnerTextField { get; set; }

		[Outlet]
		UIKit.UITextField secretKeyTextField { get; set; }

		[Outlet]
		UIKit.UIButton uploadJacketButton { get; set; }

		[Outlet]
		UIKit.UIView uploadJacketView { get; set; }

		[Outlet]
		UIKit.UILabel uploadStatusLabel { get; set; }

		[Outlet]
		UIKit.UIButton xButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (jacketIdTextField != null) {
				jacketIdTextField.Dispose ();
				jacketIdTextField = null;
			}

			if (jacketOwnerTextField != null) {
				jacketOwnerTextField.Dispose ();
				jacketOwnerTextField = null;
			}

			if (secretKeyTextField != null) {
				secretKeyTextField.Dispose ();
				secretKeyTextField = null;
			}

			if (uploadJacketButton != null) {
				uploadJacketButton.Dispose ();
				uploadJacketButton = null;
			}

			if (uploadJacketView != null) {
				uploadJacketView.Dispose ();
				uploadJacketView = null;
			}

			if (uploadStatusLabel != null) {
				uploadStatusLabel.Dispose ();
				uploadStatusLabel = null;
			}

			if (xButton != null) {
				xButton.Dispose ();
				xButton = null;
			}
		}
	}
}
