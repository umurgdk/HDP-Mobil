using System;
using ReactiveUI;
using Hdp.CoreRx.Models;
using Hdp.CoreRx.Services;
using System.Collections.Generic;
using Fusillade;

namespace Hdp.CoreRx.ViewModels.ElectionArticles
{
    public class ElectionArticlesViewModel : BaseViewModel
    {
        public ReactiveList<ElectionArticle> Articles { get; protected set; } = new ReactiveList<ElectionArticle>();

        public IReactiveDerivedList<ElectionArticleItemViewModel> ArticleItems { get; protected set; }

        private readonly IElectionArticlesService _electionArticlesService;

        public IReactiveCommand<List<ElectionArticle>> LoadCommand { get; private set; }
        public IReactiveCommand<string> PlayVideoCommand { get; private set; }

        public ElectionArticlesViewModel (IElectionArticlesService electionArticleService)
        {
            _electionArticlesService = electionArticleService;

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

            ArticleItems = Articles.CreateDerivedCollection (
                x => new ElectionArticleItemViewModel (x, gotoElectionArticle),
                orderer: (x, y) => x.CreatedAt.CompareTo (y.CreatedAt));

            LoadCommand = ReactiveCommand.CreateAsyncTask (async (param) => {
                Priority priority = Priority.Background;

                if (param != null && param is Priority)
                    priority = (Priority)param;
                
                return await _electionArticlesService.GetElectionArticles(priority);
            });

            LoadCommand.Subscribe (articles => {
                Articles.Reset();
                Articles.AddRange(articles);
            });
        }
    }
}

