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


using Hdp.CoreRx.Models;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Hdp.Droid.DroidExtensions;

using ReactiveUI;
using ReactiveUI.AndroidSupport;

namespace Hdp.Droid.Fragments
{
    public class ElectionArticlesFragment : ReactiveUI.AndroidSupport.ReactiveFragment<ElectionArticlesViewModel>
    {
        ReactiveListAdapter<ElectionArticleItemViewModel> _articlesAdapter;
        ListView _listView;

        public ElectionArticlesFragment (ElectionArticlesViewModel viewModel)
            :base()
        {
            ViewModel = viewModel;

            _articlesAdapter = new ReactiveListAdapter<ElectionArticleItemViewModel> (ViewModel.ArticleItems, (itemViewModel, viewGroup) => {
                var inflater = LayoutInflater.From(viewGroup.Context);
                var view = inflater.Inflate(Resource.Layout.ElectionArticleItemLayout, null);

                var articleImage = view.FindViewById<ImageView>(Resource.Id.articleImage);
                var articleDate = view.FindViewById<TextView>(Resource.Id.articleDate);
                var articleBody = view.FindViewById<TextView>(Resource.Id.articleBody);

                // TODO: Change view fields
                if (itemViewModel.MediaType == ElectionArticle.MediaType.Image)
                {
                    articleImage.LoadAndCacheFromUrl(itemViewModel.ImageUrl);
                }

                else if (itemViewModel.MediaType == ElectionArticle.MediaType.Video)
                {
                    articleImage.LoadAndCacheFromUrl(itemViewModel.VideoImageUrl);
                }

                articleDate.Text = itemViewModel.CreatedAt.ToString("dd MMMM yyyy");
                articleBody.Text = itemViewModel.Body;

                return view;
            });

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (_ => {
                    ViewModel.LoadCommand.Execute (null);
                });
        }

        public override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            _listView = new ListView (container.Context);
            _listView.Adapter = _articlesAdapter;
//            var view = base.OnCreateView (inflater, container, savedInstanceState);
            return _listView;
        }
    }
}

