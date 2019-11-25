using System;
using UIKit;

namespace ConsoleJackets.Services
{
    public static class AlertService
    {
        public static void ShowAlert(string title, string message)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
        }
    }
}
