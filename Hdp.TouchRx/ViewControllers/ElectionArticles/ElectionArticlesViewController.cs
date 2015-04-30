using System;
using System.Reactive.Linq;

using ReactiveUI;
using Foundation;
using UIKit;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Hdp.TouchRx.TableViewSources;
using XCDYouTubeKit;

namespace Hdp.TouchRx.ViewControllers.ElectionArticles
{
    public partial class ElectionArticlesViewController : BaseTableViewController<ElectionArticlesViewModel>
    {
        public ElectionArticlesViewController () 
            : this (UITableViewStyle.Plain)
        {
            TabBarItem.Image = new UIImage ("election-icon");

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (vm => {
                    ViewModel.PlayVideoCommand.Subscribe(videoId => {
                        var moviePlayer = new XCDYouTubeVideoPlayerViewController(videoId);
                        PresentMoviePlayerViewController(moviePlayer);
                    });
                });
        }

        public ElectionArticlesViewController (UITableViewStyle style) : base (style)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            TableView.Source = new ElectionArticlesTableViewSource (TableView, ViewModel.ArticleItems);
        }
    }
}

