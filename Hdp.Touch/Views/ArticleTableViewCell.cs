
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace Hdp.Touch.Views
{
    public partial class ArticleTableViewCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName ("ArticleTableViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("ArticleTableViewCell");

        public static readonly string Identifier = "ArticleTableViewCell";

        public ArticleTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public ArticleTableViewCell ()
        {
        }

        public static ArticleTableViewCell Create ()
        {
            return (ArticleTableViewCell)Nib.Instantiate (null, null) [0];
        }

        public void Update (string text)
        {
            categoryLabel.Text = text;
            categoryIconView.Name = text;
        }
    }
}

