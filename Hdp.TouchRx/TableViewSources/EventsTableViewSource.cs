using System;
using Hdp.CoreRx.ViewModels.Events;
using ReactiveUI;
using Hdp.TouchRx.ViewControllers.Events;
using ObjCRuntime;
using UIKit;
using CoreGraphics;

namespace Hdp.TouchRx.TableViewSources
{
    public class EventsTableViewSource : BaseTableViewSource<EventItemViewModel>
    {
        public static readonly nfloat estHeight = EventItemViewCell.EstimatedHeight;

        public static readonly EventItemViewCell demoCell = EventItemViewCell.Create();

        public EventsTableViewSource (UIKit.UITableView tableView, IReactiveNotifyCollectionChanged<EventItemViewModel> collection)
            : base (tableView, collection, EventItemViewCell.Key, estHeight)
        {
            tableView.RegisterNibForCellReuse (EventItemViewCell.Nib, EventItemViewCell.Key);
        }

        public EventsTableViewSource (UIKit.UITableView tableView) 
            : base (tableView, estHeight)
        {
            tableView.RegisterNibForCellReuse (EventItemViewCell.Nib, EventItemViewCell.Key);
        }

        public override nfloat GetHeightForRow (UIKit.UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var viewModel = (EventItemViewModel)ItemAt (indexPath);

            demoCell.ViewModel = viewModel;
            CGSize size = demoCell.ContentView.SystemLayoutSizeFittingSize (UIView.UILayoutFittingCompressedSize);
            return size.Height + 1;
        }
    }
}

