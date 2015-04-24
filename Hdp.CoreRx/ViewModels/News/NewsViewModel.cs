using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Hdp.CoreRx.Models;
using System.Threading.Tasks;
using Hdp.CoreRx.Services;
using Fusillade;
using System.Collections.Generic;
using System.Reactive;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Hdp.CoreRx.ViewModels
{
    public class NewsViewModel : BaseViewModel, ILoadingViewModel
    {
        public IReactiveDerivedList<ArticleItemViewModel> ArticleItems { get; protected set; }

        public IReactiveCommand FetchNewArticles { get; private set; }
        public IReactiveCommand LoadMoreArticles { get; private set; }

        #region ILoadingViewModel implementation
        ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading {
            get {
                return _isLoading.Value;
            }
        }
        #endregion

        HDPApp _application;

        public NewsViewModel (HDPApp application)
        {
            _application = application;

            Title = "Haberler";

            var gotoArticle = new Action<ArticleItemViewModel> (x => {
                var vm = this.CreateViewModel<ArticleViewModel>();
                vm.ArticleTitle = x.Model.Title;
                vm.Body = x.Model.Body;
                vm.ImageUrl = x.Model.ImageUrl;
                vm.CreatedAt = x.Model.CreatedAt;

                NavigateTo(vm);
            });

            ArticleItems = _application.State.Articles.CreateDerivedCollection (
                x => new ArticleItemViewModel (x, gotoArticle),
                x => true,
                (x, y) => x.Model.CreatedAt.CompareTo(y.Model.CreatedAt) * -1);

            this.WhenAnyValue (x => x.ArticleItems.Count)
                .Select (x => x == 0)
                .ToProperty (this, x => x.IsLoading, out _isLoading, true);

            FetchNewArticles = ReactiveCommand.CreateCombined (
                _application.FetchNewArticles.CanExecuteObservable, 
                _application.FetchNewArticles);
        }
    }
}

