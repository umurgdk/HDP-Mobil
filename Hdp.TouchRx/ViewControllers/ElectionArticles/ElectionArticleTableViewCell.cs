using System;
using System.Reactive.Linq;

using Foundation;
using UIKit;
using SDWebImage;

using ReactiveUI;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Hdp.CoreRx.ViewModels;
using Hdp.CoreRx.Models;

namespace Hdp.TouchRx.ViewControllers.ElectionArticles
{
    public partial class ElectionArticleTableViewCell : ReactiveTableViewCell, IViewFor<ElectionArticleItemViewModel>
    {
        public static readonly UINib Nib = UINib.FromName ("ElectionArticleTableViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("ElectionArticleTableViewCell");

        public static readonly nfloat EstimatedHeight = 264.0f;

        private string _imageUrl;
        public string ImageUrl {
            get { return _imageUrl; }
            set {
                _imageUrl = value;
                articleImage.SetImage (new NSUrl (_imageUrl));
            }
        }

        public ElectionArticleTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public static ElectionArticleTableViewCell Create ()
        {
            return (ElectionArticleTableViewCell)Nib.Instantiate (null, null) [0];
        }

        public override void WillMoveToSuperview (UIView newsuper)
        {
            base.WillMoveToSuperview (newsuper);
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            articleImage.ClipsToBounds = true;

            articleSummaryLabel.PreferredMaxLayoutWidth = 320.0f;

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (vm => {
                    if (vm.MediaType == ElectionArticle.MediaType.Image)
                    {
                        playIcon.Hidden = true;
                        ImageUrl = vm.ImageUrl;
                    }

                    else if (vm.MediaType == ElectionArticle.MediaType.Video)
                    {
                        playIcon.Hidden = false;
                        ImageUrl = vm.VideoImageUrl;
                    }

                    articleDateLabel.Text = vm.CreatedAt.ToString("dd MMMM yyyy");
                    articleSummaryLabel.Text = vm.Body;

                    SetNeedsLayout();
                    LayoutIfNeeded();
                });
                
        }

        private ElectionArticleItemViewModel _viewModel;
        public ElectionArticleItemViewModel ViewModel 
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel 
        {
            get { return _viewModel; }
            set { ViewModel = (ElectionArticleItemViewModel)value; }
        }
    }
}

