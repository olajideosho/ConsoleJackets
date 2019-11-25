using AVFoundation;
using ConsoleJackets.Models;
using ConsoleJackets.ViewControllers;
using ConsoleJackets.ViewModels;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.iOS.iCarouselBinding;

namespace ConsoleJackets
{
    public partial class LaunchViewController : UIViewController
    {
        public AVAudioPlayer audioPlayer;
        public static LaunchViewModel launchViewModel;
        public static List<JacketCardViewController> jacketCardViewControllers;
        public static LaunchViewController launchVCRef;

        public LaunchViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            launchViewModel = new LaunchViewModel();
            jacketCardViewControllers = new List<JacketCardViewController>();
            launchVCRef = this;
         
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
                DataSource = new JacketCarouselDataSource(Storyboard, jacketCardViewControllers),
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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            InvokeOnMainThread(async () =>
            {
                var connectingVC = Storyboard.InstantiateViewController("ConnectingViewController") as ConnectingViewController;
                var connectingView = connectingVC.View;
                View.AddSubview(connectingView);

                var jacketIndex = 0;
                await launchViewModel.GetRecentJacketsAsync();
                var count = await launchViewModel.GetRemaingJacketCount();
                foreach(var JCVC in jacketCardViewControllers)
                {
                    JCVC.JacketOwnerLabel.Text = launchViewModel.RecentJacketList[jacketIndex].JacketOwner;
                    JCVC.JacketIDLabel.Text = "/USR/BIN/" + launchViewModel.RecentJacketList[jacketIndex].JacketID;
                    JCVC.LocationLabel.Text = launchViewModel.RecentJacketList[jacketIndex].Location;
                    jacketIndex++;
                }
                remainingJacketLabel.Text = count.ToString();

                connectingView.RemoveFromSuperview();
            });
            

        }

        public static void ReloadDetails()
        {
            
            launchVCRef.InvokeOnMainThread(async () =>
            {
                foreach (var JCVC in jacketCardViewControllers)
                {
                    JCVC.JacketOwnerLabel.Text = "Retrieving Details...";
                    JCVC.JacketIDLabel.Text = "Retrieving Details...";
                    JCVC.LocationLabel.Text = "Retrieving Details...";
                }

                launchVCRef.remainingJacketLabel.Text = "Retrieving Details...";

                var connectingVC = launchVCRef.Storyboard.InstantiateViewController("ConnectingViewController") as ConnectingViewController;
                var connectingView = connectingVC.View;
                launchVCRef.View.AddSubview(connectingView);

                var jacketIndex = 0;
                await launchViewModel.GetRecentJacketsAsync();
                var count = await launchViewModel.GetRemaingJacketCount();
                foreach (var JCVC in jacketCardViewControllers)
                {
                    JCVC.JacketOwnerLabel.Text = launchViewModel.RecentJacketList[jacketIndex].JacketOwner;
                    JCVC.JacketIDLabel.Text = "/USR/BIN/" + launchViewModel.RecentJacketList[jacketIndex].JacketID;
                    JCVC.LocationLabel.Text = launchViewModel.RecentJacketList[jacketIndex].Location;
                    jacketIndex++;
                }

                launchVCRef.remainingJacketLabel.Text = count.ToString();

                connectingView.RemoveFromSuperview();
            });

            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            Console.WriteLine("Hello");
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
        private readonly List<JacketCardViewController> _jacketCardViewControllers;

        public JacketCarouselDataSource(UIStoryboard storyboard, List<JacketCardViewController> jacketCardViewControllers)
        {
            _storyboard = storyboard;
            _jacketCardViewControllers = jacketCardViewControllers;
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
            _jacketCardViewControllers.Add(jacketCardVC);
            //jacketCardVC.JacketOwnerLabel.Text = _JacketList[(Convert.ToInt16(index))].JacketOwner;
            //jacketCardVC.JacketIDLabel.Text = _JacketList[(Convert.ToInt16(index))].JacketID;
            //jacketCardVC.LocationLabel.Text = _JacketList[(Convert.ToInt16(index))].Location;
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