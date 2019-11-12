// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ConsoleJackets
{
	[Register ("LaunchViewController")]
	partial class LaunchViewController
	{
		[Outlet]
		UIKit.UIView jacketCarousel { get; set; }

		[Outlet]
		UIKit.UIPageControl jacketCarouselPageControl { get; set; }

		[Outlet]
		UIKit.UIView loginSignupView { get; set; }

		[Outlet]
		UIKit.UIView logoView { get; set; }

		[Outlet]
		UIKit.UIView remainingJacketView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (jacketCarousel != null) {
				jacketCarousel.Dispose ();
				jacketCarousel = null;
			}

			if (jacketCarouselPageControl != null) {
				jacketCarouselPageControl.Dispose ();
				jacketCarouselPageControl = null;
			}

			if (remainingJacketView != null) {
				remainingJacketView.Dispose ();
				remainingJacketView = null;
			}

			if (loginSignupView != null) {
				loginSignupView.Dispose ();
				loginSignupView = null;
			}

			if (logoView != null) {
				logoView.Dispose ();
				logoView = null;
			}
		}
	}
}
