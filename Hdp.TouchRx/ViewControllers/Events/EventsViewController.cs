using System;
using Hdp.CoreRx.ViewModels.Events;
using UIKit;
using Hdp.TouchRx.TableViewSources;
using Foundation;

namespace Hdp.TouchRx.ViewControllers.Events
{
    public class EventsViewController : BaseTableViewController<EventsViewModel>
    {
        public EventsViewController ()
            : this(UITableViewStyle.Plain)
        {
            TabBarItem.Image = new UIImage ("CalendarIcon");
        }

        public EventsViewController (UIKit.UITableViewStyle withStyle) : base (withStyle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            TableView.Source = new EventsTableViewSource (TableView, ViewModel.EventItems);
        }
    }
}

