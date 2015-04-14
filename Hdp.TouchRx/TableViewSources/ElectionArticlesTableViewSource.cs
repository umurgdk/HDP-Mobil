using System;

using UIKit;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Hdp.TouchRx.ViewControllers.ElectionArticles;
using ReactiveUI;
using System.Collections.Generic;
using Hdp.CoreRx.Models;
using CoreGraphics;

namespace Hdp.TouchRx.TableViewSources
{
    public class ElectionArticlesTableViewSource : BaseTableViewSource<ElectionArticleItemViewModel>
    {
        private static ElectionArticleTableViewCell _demoCell = ElectionArticleTableViewCell.Create();
        private Dictionary<int, nfloat> _cellSizeCache = new Dictionary<int, nfloat>();

        public static readonly nfloat estHeight = ElectionArticleTableViewCell.EstimatedHeight;

        public ElectionArticlesTableViewSource (UITableView tableView, IReactiveNotifyCollectionChanged<ElectionArticleItemViewModel> collection)
            : base (tableView, collection, ElectionArticleTableViewCell.Key, estHeight)
        {
            tableView.RegisterNibForCellReuse (ElectionArticleTableViewCell.Nib, ElectionArticleTableViewCell.Key);
        }

        public ElectionArticlesTableViewSource (UITableView tableView) 
            : base (tableView, estHeight)
        {
            tableView.RegisterNibForCellReuse (ElectionArticleTableViewCell.Nib, ElectionArticleTableViewCell.Key);
        }

        public override nfloat GetHeightForRow (UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            nfloat cachedSize = 0.0f;
            var model = (ElectionArticleItemViewModel) ItemAt (indexPath);

            if (_cellSizeCache.TryGetValue (model.Id, out cachedSize))
            {
                return cachedSize;
            }

            _demoCell.ViewModel = model;

            CGSize size = _demoCell.ContentView.SystemLayoutSizeFittingSize (UIView.UILayoutFittingCompressedSize);
            var height = size.Height + 1.0f;

            _cellSizeCache.Add (model.Id, height);
            return height;
        }
    }
}

