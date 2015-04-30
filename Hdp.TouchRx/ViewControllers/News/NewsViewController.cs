using System;
using System.Reactive.Linq;
using ReactiveUI;
using UIKit;

using Hdp.CoreRx.ViewModels;
using Hdp.TouchRx.TableViewSources;

namespace Hdp.TouchRx.ViewControllers
{
    public class NewsViewController : BaseTableViewController<NewsViewModel>
    {
        public NewsViewController ()
            : this(UITableViewStyle.Plain)
        {
            TabBarItem.Image = new UIImage ("NewsIcon");
        }

        public NewsViewController (UIKit.UITableViewStyle withStyle) : base (withStyle)
        {
        }
        

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            TableView.Source = new NewsTableViewSource (TableView, ViewModel.ArticleItems);
        }
    }
}

