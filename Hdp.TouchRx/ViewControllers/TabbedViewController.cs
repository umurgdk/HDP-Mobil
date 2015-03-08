using System;
using ReactiveUI;
using UIKit;
using Hdp.CoreRx.ViewModels;
using Splat;
using Hdp.TouchRx.ViewControllers.Events;
using Hdp.CoreRx.ViewModels.Events;
using CoreGraphics;

namespace Hdp.TouchRx.ViewControllers
{
    public class TabbedViewController : BaseTabbedViewController<TabsViewModel>
    {

        public TabbedViewController ()
        {
            Title = "HDP";

            var newsViewModel = new NewsViewModel ();
            var eventsViewModel = new EventsViewModel ();

            ViewControllers = new UIViewController[] {
                new NewsViewController() { ViewModel = newsViewModel },
                new EventsViewController() { ViewModel = eventsViewModel }
            };
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            TabBar.Translucent = false;
        }

        public override void DidMoveToParentViewController (UIViewController parent)
        {
            base.DidMoveToParentViewController (parent);
            NavigationController.NavigationBar.Translucent = false;
        }
    }
}

