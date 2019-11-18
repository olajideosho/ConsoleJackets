﻿using AVFoundation;
using ConsoleJackets.ViewControllers;
using CoreGraphics;
using Foundation;
using System;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace ConsoleJackets
{
    public partial class LaunchViewController : UIViewController
    {
        public AVAudioPlayer audioPlayer;
        public LaunchViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var soundUrl = new NSUrl("Sounds/bgmusic.mp3");
            NSError err;
            audioPlayer = new AVAudioPlayer(soundUrl, "Song", out err);
            audioPlayer.Volume = 0.5f;
            audioPlayer.FinishedPlaying += AudioPlayer_FinishedPlaying;
            audioPlayer.Play();

            jacketCarouselPageControl.Pages = 3;
            jacketCarouselPageControl.CurrentPage = 0;

            var logoVC = Storyboard.InstantiateViewController("ConsoleLogoAnimationViewController") as ConsoleLogoAnimationViewController;
            var consoleLogoView = logoVC.View;
            logoVC.StartAnimation(ConsoleLogoAnimationViewController.loopNumber);
            consoleLogoView.Frame = new CGRect(0, 0, 309, 48);
            logoView.AddSubview(consoleLogoView);


            var carousel = new iCarousel
            {
                ContentMode = UIViewContentMode.Left,
                Type = iCarouselType.Linear,
                Frame = new CGRect(0,0, 300, 127),
                Bounds = new CGRect(0, 0, 300, 127),
                BackgroundColor = UIColor.Clear,
                CenterItemWhenSelected = true,
                ViewpointOffset = new CGSize((nfloat)(5), (nfloat)0),
                PagingEnabled = true,
                DataSource = new JacketCarouselDataSource(Storyboard),
                Delegate = new JacketCarouselDelegate(jacketCarouselPageControl)
            };

            jacketCarousel.AddSubview(carousel);

            addJacketButton.TouchUpInside += AddJacketButton_TouchUpInside;

            buyJacketButton.TouchUpInside += BuyJacketButton_TouchUpInside;

            searchJacketByIdButton.TouchUpInside += SearchJacketByIdButton_TouchUpInside;

            aboutButton.TouchUpInside += AboutButton_TouchUpInside;

            NavigationController.NavigationBar.Hidden = true;

            remainingJacketView.Layer.CornerRadius = 10;
            remainingJacketView.ClipsToBounds = true;

            loginSignupView.Layer.CornerRadius = 10;
            loginSignupView.ClipsToBounds = true;

        }

        private void AboutButton_TouchUpInside(object sender, EventArgs e)
        {
            var aboutVC = Storyboard.InstantiateViewController("AboutViewController") as AboutViewController;
            aboutVC.View.BackgroundColor = UIColor.Black.ColorWithAlpha((nfloat)0.5);
            aboutVC.ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen;

            PresentViewController(aboutVC, true, null);
        }

        private void SearchJacketByIdButton_TouchUpInside(object sender, EventArgs e)
        {
            var searchJacketVC = Storyboard.InstantiateViewController("SearchJacketViewController") as SearchJacketViewController;
            searchJacketVC.View.BackgroundColor = UIColor.Black.ColorWithAlpha((nfloat)0.5);
            searchJacketVC.ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen;

            PresentViewController(searchJacketVC, true, null);
        }

        private void BuyJacketButton_TouchUpInside(object sender, EventArgs e)
        {
            var buyJacketVC = Storyboard.InstantiateViewController("BuyJacketViewController") as BuyJacketViewController;
            buyJacketVC.View.BackgroundColor = UIColor.Black.ColorWithAlpha((nfloat)0.5);
            buyJacketVC.ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen;

            PresentViewController(buyJacketVC, true, null);
        }

        private void AddJacketButton_TouchUpInside(object sender, EventArgs e)
        {
            var uploadJacketVC = Storyboard.InstantiateViewController("UploadJacketViewController") as UploadJacketViewController;
            uploadJacketVC.View.BackgroundColor = UIColor.Black.ColorWithAlpha((nfloat)0.5);
            uploadJacketVC.ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen;

            PresentViewController(uploadJacketVC, true, null);
        }

        private void AudioPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            audioPlayer.Play();
        }
    }

    internal class JacketCarouselDelegate : iCarouselDelegate
    {
        private readonly UIPageControl _pageControl;

        public JacketCarouselDelegate(UIPageControl pageControl)
        {
            _pageControl = pageControl;
        }

        [Export("carousel:valueForOption:withDefault:")]
        public nfloat ValueForOption(iCarousel carousel, iCarouselOption option, nfloat value)
        {
            switch (option)
            {
                case iCarouselOption.Spacing:
                    return (nfloat)(value * 1.1);
                case iCarouselOption.Wrap:
                    return (nfloat)0.0;
                default:
                    return value;
            }
        }

        [Export("carouselDidScroll:")]
        public void CarouselDidScroll(iCarousel carousel)
        {
            _pageControl.CurrentPage = carousel.CurrentItemIndex;
        }
    }

    internal class JacketCarouselDataSource : iCarouselDataSource
    {
        private readonly UIStoryboard _storyboard;

        public JacketCarouselDataSource(UIStoryboard storyboard)
        {
            _storyboard = storyboard;
        }

        public override nint NumberOfItemsInCarousel(iCarousel carousel)
        {
            //throw new NotImplementedException();
            return 3;
        }

        public override UIView ViewForItemAtIndex(iCarousel carousel, nint index, UIView view)
        {
            //throw new NotImplementedException();
            var jacketCardVC = _storyboard.InstantiateViewController("JacketCardViewController") as JacketCardViewController;
            var jacketCardView = jacketCardVC.View;
            var outerView = new UIView()
            {
                Frame = new CGRect(0, 0, 270, 127),
                Bounds = new CGRect(0,0,270,127)
            };
            //Implement settings for each view

            jacketCardView.Frame = new CGRect(0, 0, 270, 127);
            outerView.ClipsToBounds = false;
            outerView.Layer.ShadowColor = UIColor.Black.CGColor;
            outerView.Layer.ShadowOpacity = (float)0.5;
            outerView.Layer.ShadowOffset = new CoreGraphics.CGSize(-2, -2);
            outerView.Layer.ShadowRadius = (nfloat)10.0;
            outerView.Layer.ShadowPath = UIBezierPath.FromRect(new CoreGraphics.CGRect(0, 0, 275, 132)).CGPath;
            outerView.Layer.ShouldRasterize = true;
            outerView.AddSubview(jacketCardView);

            return outerView;
        }
    }
}