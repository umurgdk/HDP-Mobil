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
//            demoCell.SetNeedsLayout ();
//            demoCell.LayoutIfNeeded ();
            CGSize size = demoCell.ContentView.SystemLayoutSizeFittingSize (UIView.UILayoutFittingCompressedSize);
            return size.Height + 1;

//            if (cell != null) {
//                cell.SetNeedsLayout ();
//                cell.LayoutIfNeeded ();
////                return cell.Bounds.Height;
////                var size = cell.SizeThatFits (new CGSize (cell.ContentView.Bounds.Width, 1000));
//                CGSize size = cell.ContentView.SystemLayoutSizeFittingSize (UIView.UILayoutFittingCompressedSize);
//                return size.Height + 1;
//                return cell.ContentView.Bounds.Height;
//            }
//
//            return base.GetHeightForRow (tableView, indexPath);
        }
    }
}

