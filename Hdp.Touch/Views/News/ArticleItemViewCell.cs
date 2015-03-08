using System;
using System.Drawing;

using Foundation;
using UIKit;
using SDWebImage;
using Cirrious.MvvmCross.Binding.Touch.Views;


namespace Hdp.Touch.Views.News
{
    public partial class ArticleItemViewCell : MvxTableViewCell
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
        }

        public ArticleItemViewCell (IntPtr handle) : base (handle)
        {
        }
    }
}

