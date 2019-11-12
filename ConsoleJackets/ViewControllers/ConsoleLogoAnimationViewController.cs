using CoreGraphics;
using Foundation;
using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace ConsoleJackets
{
    public partial class ConsoleLogoAnimationViewController : UIViewController
    {
        private nfloat leftOInitialPosition;
        private nfloat rightOInitialPosition;
        
        public ConsoleLogoAnimationViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitialState();

            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            StartAnimation(3);

            Console.WriteLine("Hello");
        }

        private async Task StartAnimation(int loop)
        {
            foreach(int i in Enumerable.Range(1, loop))
            {
                await Task.Delay(3000);
                //All Disappear Except from Os.
                double duration = 0.4;
                double delay = 0.8;

                UIView.Animate(duration, delay, UIViewAnimationOptions.CurveEaseInOut, animation: () =>
                {

                    leftDash.Alpha = 0;
                    leftC.Alpha = 0;
                    leftN.Alpha = 0;
                    midS.Alpha = 0;
                    rightL.Alpha = 0;
                    rightE.Alpha = 0;
                    rightDash.Alpha = 0;
                    View.LayoutIfNeeded();

                }, completion: () =>
                {
                    //Os Dissapear while moving to intersection
                    leftOPositionControl.Constant = -57;
                    rightOPositionControl.Constant = -26;
                    delay = 0.3;

                    UIView.Animate(duration, delay, UIViewAnimationOptions.CurveEaseInOut, animation: () =>
                    {
                        leftO.Alpha = 0;
                        rightO.Alpha = 0;
                        View.LayoutIfNeeded();
                    }, completion: () =>
                    {
                        //Os Reappear
                        delay = 0.1;

                        UIView.Animate(duration, delay, UIViewAnimationOptions.CurveEaseInOut, animation: () =>
                        {

                            rightO.Alpha = 1;
                            leftO.Alpha = 1;
                            View.LayoutIfNeeded();

                        }, completion: () =>
                        {
                            //Os Move to Original Position while others reappear

                            leftOPositionControl.Constant = leftOInitialPosition;
                            rightOPositionControl.Constant = rightOInitialPosition;
                            delay = 0.0;

                            UIView.Animate(duration, delay, UIViewAnimationOptions.CurveEaseInOut, animation: () =>
                            {

                                leftDash.Alpha = 1;
                                leftC.Alpha = 1;
                                leftO.Alpha = 1;
                                leftN.Alpha = 1;
                                midS.Alpha = 1;
                                rightO.Alpha = 1;
                                rightL.Alpha = 1;
                                rightE.Alpha = 1;
                                rightDash.Alpha = 1;
                                View.LayoutIfNeeded();

                            }, completion: () =>
                            {
                            });

                        });

                    });

                });
            }
            

        }

        private void InitialState()
        {
            //Setting the initial state of the logo
            //leftDash.Hidden = true;
            //leftC.Hidden = true;
            //leftN.Hidden = true;
            //midS.Hidden = true;
            //rightL.Hidden = true;
            //rightE.Hidden = true;
            //rightDash.Hidden = true;

            leftO.TranslatesAutoresizingMaskIntoConstraints = false;

            rightO.TranslatesAutoresizingMaskIntoConstraints = false;

            leftOInitialPosition = leftOPositionControl.Constant;
            rightOInitialPosition = rightOPositionControl.Constant;

            
        }
    }
}