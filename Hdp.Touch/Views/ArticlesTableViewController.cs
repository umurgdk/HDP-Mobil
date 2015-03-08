using System;
using System.Drawing;

using Foundation;
using UIKit;

using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

using Hdp.Core.ViewModels;
using CoreGraphics;

namespace Hdp.Touch.Views
{
    public class ArticlesTableViewController : MvxTableViewController
    {
        public new ArticlesViewModel ViewModel
        {
            get { return (ArticlesViewModel) base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public ArticlesTableViewController () : base (UITableViewStyle.Plain)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			
            // Register the TableView's data source
            // TableView.Source = new ArticlesTableViewControllerSource ();

            var source = new ArticlesTableSource (TableView);

            TableView.Source = source;
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

            var set = this.CreateBindingSet<ArticlesTableViewController, ArticlesViewModel> ();
            set.Bind (source).To (vm => vm.Articles);
            set.Apply ();

            TableView.ReloadData ();
        }
    }

    public class ArticlesTableSource : MvxTableViewSource
    {
        static readonly string CellIdentifier = ArticleTableViewCell.Identifier;

        public ArticlesTableSource (UITableView tableView) : base (tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
        {
            ArticleTableViewCell cell = (ArticleTableViewCell)tableView.DequeueReusableCell (CellIdentifier);

            if (cell == null) {
                cell = ArticleTableViewCell.Create();
            }

            cell.Update (item.ToString ());

            return cell;
        }
    }
}

