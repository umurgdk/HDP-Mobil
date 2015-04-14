using System;
using System.Drawing;

using Foundation;
using UIKit;
using SDWebImage;

using ReactiveUI;
using System.Reactive.Linq;

using Hdp.CoreRx.ViewModels;

namespace Hdp.TouchRx.Views.News
{
    public partial class ArticleItemViewCell : ReactiveTableViewCell, IViewFor<ArticleItemViewModel>
    {
        public static readonly UINib Nib = UINib.FromName ("ArticleItemViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("ArticleItemViewCell");

        public string imageUrl;
        public string ImageUrl {
            get { return imageUrl; }
            set {
                thumbnailImageView.SetImage(new NSUrl(value));
                imageUrl = value;
            }
        }

        public static ArticleItemViewCell Create ()
        {
            return (ArticleItemViewCell)Nib.Instantiate (null, null) [0];
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            thumbnailImageView.ClipsToBounds = true;

            this.WhenAnyValue (x => x.ViewModel)
                .Where(x => x != null)
                .Subscribe (x => {
                    titleLabel.Text = x.Model.Title;
                    categoryLabel.Text = x.Model.Category;
                    ImageUrl = x.Model.ImageUrl;
                });
        }

        private ArticleItemViewModel _viewModel;
        public ArticleItemViewModel ViewModel 
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel 
        {
            get { return _viewModel; }
            set { ViewModel = (ArticleItemViewModel)value; }
        }

        public ArticleItemViewCell (IntPtr handle) : base (handle)
        {
        }
    }
}

