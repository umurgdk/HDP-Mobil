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
using Hdp.CoreRx.Services;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Hdp.TouchRx.ViewControllers.ElectionArticles;

namespace Hdp.TouchRx.ViewControllers
{
    public class TabbedViewController : BaseTabbedViewController<TabsViewModel>
    {
        IServiceConstructor _serviceConstructor;

        public TabbedViewController ()
        {
            _serviceConstructor = Locator.Current.GetService<IServiceConstructor>();

            Title = "HDP";

            var newsViewModel = _serviceConstructor.Construct<NewsViewModel>();
            var eventsViewModel = _serviceConstructor.Construct<EventsViewModel> ();
            var electionArticlesViewModel = _serviceConstructor.Construct<ElectionArticlesViewModel> ();
            var organizationMenuViewModel = _serviceConstructor.Construct<OrganizationMenuViewModel> ();

            ViewControllers = new UIViewController[] {
                new ElectionArticlesViewController() { ViewModel = electionArticlesViewModel },
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

