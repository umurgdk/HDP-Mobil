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
        UIScrollView scrollView;
        UIImageView imageView;
        UILabel titleLabel;
        ArticleWebView bodyView;

        public ArticleViewController ()
            : base()
        {
            imageView = new UIImageView ();
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;

            titleLabel = new UILabel ();
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            bodyView = new ArticleWebView ();
            bodyView.TranslatesAutoresizingMaskIntoConstraints = false;

            scrollView = new UIScrollView ();
        }

        private void BuildUI ()
        {
            View.BackgroundColor = UIColor.White;

            imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            imageView.ClipsToBounds = true;

            titleLabel.Font = UIFont.PreferredHeadline;
            titleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            titleLabel.Lines = 0;

            bodyView.LoadFinished += WebViewDidLoad;

            scrollView.ScrollEnabled = true;
            scrollView.AlwaysBounceVertical = true;
            scrollView.Frame = View.Frame;
            scrollView.AddSubviews (imageView, titleLabel, bodyView);

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
        }

        private void WebViewDidLoad (object sender, EventArgs e)
        {
            var frame = bodyView.Frame;
            frame.Width = View.Bounds.Width - 32.0f;
            frame.Height = 1;
            bodyView.Frame = frame;
            CGSize fittingSize = bodyView.SizeThatFits (new CGSize (0, 0));
            frame.X = 16.0f;
            frame.Y = titleLabel.Frame.Bottom + 16.0f;
            frame.Size = fittingSize;
            bodyView.Frame = frame;

            UpdateScroll ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            BuildUI ();
            BuildConstraints ();

            var topItem = NavigationController.NavigationBar.TopItem;
            topItem.Title = "Haberler";

            titleLabel.LineBreakMode = UILineBreakMode.WordWrap;

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    this.imageView.SetImage(new NSUrl(x.ImageUrl), null, SDWebImageOptions.ProgressiveDownload);
                    this.titleLabel.Text = x.ArticleTitle;
                    this.bodyView.LoadHtmlString (x.Body, new NSUrl("http://localhost:3000/"));
                    this.Title = x.ArticleTitle;
                });
        }

        private void UpdateScroll ()
        {
            scrollView.ContentSize = new CGSize(View.Bounds.Width, bodyView.Frame.Bottom + 86.0f);
        }
    }
}

