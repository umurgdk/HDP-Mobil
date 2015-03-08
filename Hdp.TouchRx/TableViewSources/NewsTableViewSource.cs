using System;
using ReactiveUI;
using Hdp.CoreRx.ViewModels;
using UIKit;
using Foundation;
using Hdp.TouchRx.Views.News;

namespace Hdp.TouchRx.TableViewSources
{
    public class NewsTableViewSource : BaseTableViewSource<ArticleItemViewModel>
    {
        public NewsTableViewSource (UIKit.UITableView tableView, IReactiveNotifyCollectionChanged<ArticleItemViewModel> collection)
            : base (tableView, collection, ArticleItemViewCell.Key, 170.0f)
        {
            tableView.RegisterNibForCellReuse (ArticleItemViewCell.Nib, ArticleItemViewCell.Key);
        }

        public NewsTableViewSource (UIKit.UITableView tableView) 
            : base (tableView, 170.0f)
        {
            tableView.RegisterNibForCellReuse (ArticleItemViewCell.Nib, ArticleItemViewCell.Key);
        }
    }
}

