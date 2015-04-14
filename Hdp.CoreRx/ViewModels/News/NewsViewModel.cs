using System;
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
    public class NewsViewModel : BaseViewModel
    {
        public ReactiveList<Models.Article> Articles { get; protected set; } = new ReactiveList<Models.Article>();

        public IReactiveDerivedList<ArticleItemViewModel> ArticleItems { get; protected set; }
        public IReactiveDerivedList<ArticleItemViewModel> VisibleArticles { get; protected set; }

        private readonly INewsService _newsService;

        public IReactiveCommand<List<Article>> LoadCommand { get; private set; }

        public NewsViewModel (INewsService newsService)
        {
            _newsService = newsService;

            Title = "Haberler";

            var gotoArticle = new Action<ArticleItemViewModel> (x => {
                var vm = this.CreateViewModel<ArticleViewModel>();
                vm.ArticleTitle = x.Model.Title;
                vm.Body = x.Model.Body;
                vm.ImageUrl = x.Model.ImageUrl;

                NavigateTo(vm);
            });

            ArticleItems = Articles.CreateDerivedCollection (
                x => new ArticleItemViewModel (x, gotoArticle),
                x => true,
                (x, y) => x.Model.Title.CompareTo(y.Model.Title));

            VisibleArticles = ArticleItems.CreateDerivedCollection (
                x => x,
                x => !x.IsHidden);

            LoadCommand = ReactiveCommand.CreateAsyncTask (async _ => {
                return await _newsService.GetArticles (Priority.Background).ConfigureAwait (false);
            });

            LoadCommand.Subscribe (articles => {
                Articles.Reset();
                Articles.AddRange(articles);
            });
        }
    }
}

