using System;
using System.Drawing;
using System.Reactive.Linq;

using ReactiveUI;

using Foundation;
using UIKit;
using SDWebImage;

using Hdp.CoreRx.ViewModels;
using CoreGraphics;

namespace Hdp.TouchRx.ViewControllers.News
{
    public partial class ArticleViewController : BaseViewController<ArticleViewModel>
    {
        nfloat imageHeight;

        UIScrollView scrollView;
        UIImageView imageView;
        UILabel titleLabel;
        UILabel bodyLabel;

        public ArticleViewController ()
            : base()
        {
            imageView = new UIImageView ();
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;

            titleLabel = new UILabel ();
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            bodyLabel = new UILabel ();
            bodyLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            scrollView = new UIScrollView ();
        }

        private void BuildUI ()
        {
            View.BackgroundColor = UIColor.White;

            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            titleLabel.Font = UIFont.PreferredHeadline;
            titleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            titleLabel.Lines = 0;

            bodyLabel.Font = UIFont.PreferredBody;
            bodyLabel.LineBreakMode = UILineBreakMode.WordWrap;
            bodyLabel.Lines = 0;

            scrollView.ScrollEnabled = true;
            scrollView.AlwaysBounceVertical = true;
            scrollView.Frame = View.Frame;
            scrollView.AddSubviews (imageView, titleLabel, bodyLabel);

            View.AddSubview (scrollView);
        }

        private void BuildConstraints ()
        {
            View.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1.0f, 1.0f));
            View.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1.0f, 200.0f));
            View.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1.0f, 0.0f));
            View.AddConstraint (NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Top, 1.0f, 0));

            View.AddConstraint (NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, imageView, NSLayoutAttribute.Bottom, 1.0f, 16.0f));
            View.AddConstraint (NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeftMargin, 1.0f, 0.0f));
            View.AddConstraint (NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.RightMargin, 1.0f, 0.0f));

            View.AddConstraint (NSLayoutConstraint.Create (bodyLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, titleLabel, NSLayoutAttribute.Bottom, 1.0f, 16.0f));
            View.AddConstraint (NSLayoutConstraint.Create (bodyLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.LeftMargin, 1.0f, 0.0f));
            View.AddConstraint (NSLayoutConstraint.Create (bodyLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.RightMargin, 1.0f, 0.0f));
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            BuildUI ();
            BuildConstraints ();

            imageHeight = this.imageView.Frame.Height;

            var topItem = NavigationController.NavigationBar.TopItem;
            topItem.Title = "Haberler";

            titleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            bodyLabel.LineBreakMode = UILineBreakMode.WordWrap;

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    this.imageView.SetImage(new NSUrl(x.ImageUrl), null, SDWebImageOptions.ProgressiveDownload);
                    this.titleLabel.Text = x.ArticleTitle;
                    this.bodyLabel.Text = x.Body;
                    this.Title = x.ArticleTitle;
                });
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewWillAppear (animated);
            UpdateScroll ();
        }

        private void UpdateScroll ()
        {
            scrollView.ContentSize = new CGSize(View.Bounds.Width, bodyLabel.Frame.Bottom + 16.0f);
        }
    }
}

