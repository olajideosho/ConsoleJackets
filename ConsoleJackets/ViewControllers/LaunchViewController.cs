using AVFoundation;
using CoreGraphics;
using Foundation;
using System;
using UIKit;

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

            var logoVC = Storyboard.InstantiateViewController("ConsoleLogoAnimationViewController") as ConsoleLogoAnimationViewController;
            var consoleLogoView = logoVC.View;
            consoleLogoView.Frame = new CGRect(0, 0, 309, 48);
            logoView.AddSubview(consoleLogoView);
        }

        private void AudioPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            audioPlayer.Play();
        }
    }
}