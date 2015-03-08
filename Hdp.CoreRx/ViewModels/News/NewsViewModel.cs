using System;
using ReactiveUI;
using Hdp.CoreRx.Models;
using System.Threading.Tasks;

namespace Hdp.CoreRx.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public ReactiveList<Models.Article> Articles { get; protected set; } = new ReactiveList<Models.Article>();

        public IReactiveDerivedList<ArticleItemViewModel> ArticleItems;
        public IReactiveDerivedList<ArticleItemViewModel> VisibleArticles;

        public NewsViewModel ()
        {
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

            for (int i = 0; i < 20; i++) {
                Articles.Add (Article.Create ("Article " + i.ToString ()));
            }
        }
    }
}

