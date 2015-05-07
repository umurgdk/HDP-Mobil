using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using ReactiveUI.AndroidSupport;

using Hdp.CoreRx.ViewModels;
using Hdp.Droid.DroidExtensions;
using Splat;
using Hdp.CoreRx.Services;
using Android.Support.V4.App;
using Android.Graphics;
using Android.Support.V4.Widget;
using System.Globalization;

namespace Hdp.Droid.Fragments
{
    public class NewsArticlesFragment : BaseLoadingFragment<NewsViewModel>
    {
        ReactiveListAdapter<ArticleItemViewModel> _newsAdapter;

        ListView _listView;
        SwipeRefreshLayout _refreshLayout;

        public NewsArticlesFragment ()
        {
            var serviceConstructor = Locator.Current.GetService<IServiceConstructor> ();
            ViewModel = serviceConstructor.Construct<NewsViewModel> ();

            _newsAdapter = new ReactiveListAdapter<ArticleItemViewModel> (ViewModel.ArticleItems, CreateItemView, InitItemView);

            if (ViewModel is IRoutingViewModel)
            {
                ViewModel.RequestNavigation.Subscribe (x => {
                    var viewModelViewService = Locator.Current.GetService<IViewModelViewService>();
                    var viewType = viewModelViewService.GetViewFor(x.GetType());
                    var view = (IViewFor)serviceConstructor.Construct(viewType);

                    view.ViewModel = x;

                    var fragment = view as ReadArticleFragment;

                    Activity.SupportFragmentManager.BeginTransaction()
                        .Add(Resource.Id.mainFrame, fragment, "MODAL")
                        .AddToBackStack(x.Title)
                        .Commit();
                });
            }
        }

        protected View CreateItemView (ArticleItemViewModel itemViewModel, ViewGroup viewGroup)
        {
            var inflater = LayoutInflater.From(viewGroup.Context);
            return inflater.Inflate(Resource.Layout.ArticleItemLayout, null);
        }

        protected void InitItemView (ArticleItemViewModel itemViewModel, View view)
        {
            var articleImage = view.FindViewById<ImageView>(Resource.Id.articleImage);
            var articleCategory = view.FindViewById<TextView>(Resource.Id.articleCategory);
            var articleTitle = view.FindViewById<TextView>(Resource.Id.articleTitle);
            var articleDate = view.FindViewById<TextView>(Resource.Id.articleDate);
            var articleSummary = view.FindViewById<TextView>(Resource.Id.articleSummary);

            if (itemViewModel.ImageUrl.Length > 0)
            {
                Koush.UrlImageViewHelper.SetUrlDrawable(articleImage, itemViewModel.ImageUrl);
            }

            articleCategory.Text = itemViewModel.Category;
            articleTitle.Text = itemViewModel.Title;

            articleDate.Text = itemViewModel.CreatedAt.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));

            articleSummary.Text = itemViewModel.Summary;
        }

        protected override View OnCreateContentView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _refreshLayout = new SwipeRefreshLayout (container.Context);
            _refreshLayout.Refresh += (sender, e) => ViewModel.RefreshContent.Execute(null);

            ViewModel.RefreshContent.IsExecuting.BindTo (_refreshLayout, x => x.Refreshing);

            _listView = new ListView (container.Context);
            _listView.SetSelector (Resource.Color.transparent);
            _listView.Adapter = _newsAdapter;
            _listView.Divider = null;

            _listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                var itemViewModel = ViewModel.ArticleItems[e.Position];
                itemViewModel.GoToCommand.Execute(null);
            };

            _refreshLayout.AddView (_listView);

            return _refreshLayout;
        }
    }
}

