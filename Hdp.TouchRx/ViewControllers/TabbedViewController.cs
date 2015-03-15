using System;
using ReactiveUI;
using UIKit;
using Hdp.CoreRx.ViewModels;
using Splat;
using Hdp.TouchRx.ViewControllers.Events;
using Hdp.CoreRx.ViewModels.Events;
using CoreGraphics;
using Foundation;
using Hdp.TouchRx.ViewControllers.Organization;
using Hdp.CoreRx.ViewModels.Organization;

namespace Hdp.TouchRx.ViewControllers
{
    public class TabbedViewController : BaseTabbedViewController<TabsViewModel>
    {

        public TabbedViewController ()
        {
            Title = "HDP";

            var newsViewModel = new NewsViewModel ();
            var eventsViewModel = new EventsViewModel ();
            var organizationMenuViewModel = new OrganizationMenuViewModel ();

            ViewControllers = new UIViewController[] {
                new NewsViewController() { ViewModel = newsViewModel },
                new EventsViewController() { ViewModel = eventsViewModel },
                new OrganizationsMenuCollectionViewController() { ViewModel = organizationMenuViewModel }
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

            NSArray contents = NSBundle.MainBundle.LoadNib ("TitleView", null, null);
            var view = contents.GetItem<UIView> (contents.Count - 1);

            NavigationItem.TitleView = view;
        }
    }
}

