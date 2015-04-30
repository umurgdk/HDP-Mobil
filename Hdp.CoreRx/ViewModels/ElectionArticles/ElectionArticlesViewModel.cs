using System;
using ReactiveUI;
using Hdp.CoreRx.Models;
using Hdp.CoreRx.Services;
using System.Collections.Generic;
using Fusillade;
using System.Reactive.Linq;

namespace Hdp.CoreRx.ViewModels.ElectionArticles
{
    public class ElectionArticlesViewModel : BaseViewModel, ILoadingViewModel, IRefreshViewModel
    {
        public IReactiveDerivedList<ElectionArticleItemViewModel> ArticleItems { get; protected set; }

        public IReactiveCommand<object> RefreshContent { get; private set; }
        public IReactiveCommand<string> PlayVideoCommand { get; private set; }

        #region ILoadingViewModel implementation
        private ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading {
            get { return this._isLoading.Value; }
        }
        #endregion


        public ElectionArticlesViewModel (HDPApp _app)
        {

            Title = "Secim";

            PlayVideoCommand = ReactiveCommand.CreateAsyncTask (async videoId => {
                return (string)videoId;
            });

            var gotoElectionArticle = new Action<ElectionArticleItemViewModel> (x => {
                if (x.MediaType == ElectionArticle.MediaType.Video)
                {
                    PlayVideoCommand.Execute(x.VideoId);
                }
            });

            ArticleItems = _app.State.ElectionArticles.CreateDerivedCollection (
                x => new ElectionArticleItemViewModel (x, gotoElectionArticle, true),
                orderer: (x, y) => x.CreatedAt.CompareTo (y.CreatedAt) * -1);


            this.WhenAnyValue (x => x.ArticleItems.Count)
                .Select (x => x == 0)
                .ToProperty (this, x => x.IsLoading, out _isLoading, true);

            RefreshContent = ReactiveCommand.CreateCombined (
                _app.FetchNewElectionArticles.CanExecuteObservable, 
                _app.FetchNewElectionArticles);
        }
    }
}

