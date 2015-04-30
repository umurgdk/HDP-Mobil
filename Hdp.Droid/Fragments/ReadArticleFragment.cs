using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using ReactiveUI.AndroidSupport;

using Hdp.CoreRx.ViewModels;
using Hdp.Droid.DroidExtensions;
using Android.Webkit;
using System.Globalization;

namespace Hdp.Droid.Fragments
{
    public class ReadArticleFragment : ReactiveUI.AndroidSupport.ReactiveFragment<ArticleViewModel>
    {
        ImageView _articleImage;
        TextView _articleDate;
        TextView _articleTitle;

        WebView _articleBody;

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate (Resource.Layout.ArticleReadLayout, null);

            _articleImage = view.FindViewById<ImageView> (Resource.Id.articleImage);
            _articleDate = view.FindViewById<TextView> (Resource.Id.articleDate);
            _articleTitle = view.FindViewById<TextView> (Resource.Id.articleTitle);
            _articleBody = view.FindViewById<WebView> (Resource.Id.articleBodyWeb);

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    Koush.UrlImageViewHelper.SetUrlDrawable(_articleImage, ViewModel.ImageUrl);
                    _articleDate.Text = ViewModel.CreatedAt.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));
                    _articleTitle.Text = ViewModel.ArticleTitle;
                        
                    _articleBody.Settings.JavaScriptEnabled = true;
                    _articleBody.LoadData (PrepareHtml(ViewModel.Body), "text/html; charset=UTF-8", null);
                });

            return view;
        }

        public override void OnAttach (Android.App.Activity activity)
        {
            base.OnAttach (activity);

            var mainActivity = (MainActivity)activity;
            if (mainActivity != null)
            {
                mainActivity.IsBackButtonVisible = true;
            }
        }

        private string PrepareHtml (string html)
        {
            return @"
            <html><head>
            <style> body {margin: 0; padding: 0; color: #444; } </style>
            </haed><body>" + html + @"</body></html>";
        }
    }
}

