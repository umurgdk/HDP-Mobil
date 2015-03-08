using System;
using UIKit;

namespace Hdp.TouchRx.ViewControllers
{
    public class HdpRootViewController : UINavigationController
    {
        public HdpRootViewController ()
        {
            Title = "HDP";

            var tabbedViewController = new TabbedViewController ();
            View.AddSubview (tabbedViewController.View);


            PresentViewController (tabbedViewController, false, null);
        }
    }
}

