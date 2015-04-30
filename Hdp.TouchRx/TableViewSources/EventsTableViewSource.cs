using System;
using Hdp.CoreRx.ViewModels.Events;
using ReactiveUI;
using Hdp.TouchRx.ViewControllers.Events;
using ObjCRuntime;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace Hdp.TouchRx.TableViewSources
{
    public class EventsTableViewSource : BaseTableViewSource<EventItemViewModel>
    {
        public static readonly nfloat estHeight = EventItemViewCell.EstimatedHeight;

        public static readonly EventItemViewCell demoCell = EventItemViewCell.Create();
        private Dictionary<int, nfloat> _cellSizeCache = new Dictionary<int, nfloat>();

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
            nfloat cachedSize = 0.0f;
            var viewModel = (EventItemViewModel)ItemAt (indexPath);

            if (_cellSizeCache.TryGetValue(viewModel.Id, out cachedSize))
            {
                return cachedSize;
            }

            demoCell.ViewModel = viewModel;
            CGSize size = demoCell.ContentView.SystemLayoutSizeFittingSize (UIView.UILayoutFittingCompressedSize);
            var height = size.Height + 1;

            _cellSizeCache.Add (viewModel.Id, height);
            return height;
        }
    }
}

