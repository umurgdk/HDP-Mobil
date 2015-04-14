using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Hdp.TouchRx.ViewControllers;
using Splat;
using Hdp.CoreRx.Services;
using Hdp.CoreRx.ViewModels;
using Hdp.TouchRx.Services;
using Hdp.CoreRx;

namespace Hdp.TouchRx
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow (UIScreen.MainScreen.Bounds);
			
            // If you have defined a root view controller, set it here:
            // window.RootViewController = myViewController;
            Locator.CurrentMutable.InitializeServices ();

            var viewModelViews = Locator.Current.GetService<IViewModelViewService> ();
            viewModelViews.RegisterViewModels (typeof(NewsViewController).Assembly);

            DeviceType deviceType;
            nfloat scale = UIScreen.MainScreen.Scale;

            if (scale == 1.0)
            {
                deviceType = DeviceType.ios;
            } 
            else if (scale == 2.0)
            {
                deviceType = DeviceType.ios2x;
            }
            else
            {
                deviceType = DeviceType.ios3x;
            }

            var hdpApp = new HDPApp(deviceType);

            window.RootViewController = new UINavigationController (new TabbedViewController());
            // make the window visible
            window.MakeKeyAndVisible ();
			
            return true;
        }
    }
}

