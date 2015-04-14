using System;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Linq;
using Hdp.CoreRx.Models;
using Fusillade;
using System.Collections.Generic;
using Akavache;
using Connectivity.Plugin;
using Polly;
using System.Net;
using System.Diagnostics;

namespace Hdp.CoreRx.Services
{
    public class NewsService : INewsService
    {
        private readonly IApiService _apiService;

        public NewsService (IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Article>> GetArticles (Priority priority)
        {
            var cache = BlobCache.LocalMachine;
            var cachedArticles = cache.GetAndFetchLatest ("articles", () => GetRemoteArticlesAsync (priority));

            var articles = await cachedArticles.FirstOrDefaultAsync ();
            return articles;
        }

        public async Task<Article> GetArticle (Priority priority, int id)
        {
            var cache = BlobCache.LocalMachine;
            var cachedArticle = cache.GetAndFetchLatest ("article-" + id.ToString(), () => GetRemoteArticleAsync (priority, id));

            var article = await cachedArticle.FirstOrDefaultAsync ();
            return article;
        }

        private async Task<List<Article>> GetRemoteArticlesAsync (Priority priority)
        {
            List<Article> articles = null;
            Task<List<Article>> getArticlesTask;

            switch (priority) {
            case Priority.Background:
                getArticlesTask = _apiService.Background.GetArticles (ApiService.Device);
                break;
            case Priority.UserInitiated:
                getArticlesTask = _apiService.UserInitiated.GetArticles (ApiService.Device);
                break;
            case Priority.Speculative:
                getArticlesTask = _apiService.Speculative.GetArticles (ApiService.Device);
                break;
            default:
                getArticlesTask = _apiService.UserInitiated.GetArticles (ApiService.Device);
                break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                articles = await Policy
                    .Handle<WebException> ()
                    .WaitAndRetryAsync (5, retryAttempt => TimeSpan.FromSeconds (Math.Pow (2, retryAttempt)))
                    .ExecuteAsync(async () => await getArticlesTask);
            }

            return articles.Select(article => {
                article.ImageUrl = ApiService.ApiBaseAddress + article.ImageUrl;
                return article;
            }).ToList();
        }

        private async Task<Article> GetRemoteArticleAsync (Priority priority, int id)
        {
            Article article = null;
            Task<Article> getArticleTask;

            switch (priority) {
            case Priority.Background:
                getArticleTask = _apiService.Background.GetArticleById (id, ApiService.Device);
                break;
            case Priority.UserInitiated:
                getArticleTask = _apiService.UserInitiated.GetArticleById (id, ApiService.Device);
                break;
            case Priority.Speculative:
                getArticleTask = _apiService.Speculative.GetArticleById (id, ApiService.Device);
                break;
            default:
                getArticleTask = _apiService.UserInitiated.GetArticleById (id, ApiService.Device);
                break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                article = await Policy
                    .Handle<WebException> ()
                    .WaitAndRetryAsync (5, retryAttempt => TimeSpan.FromSeconds (Math.Pow (2, retryAttempt)))
                    .ExecuteAsync(async () => await getArticleTask);
            }

            return article;
        }
    }

    public interface INewsService
    {
        Task<List<Article>> GetArticles (Priority priority);
        Task<Article> GetArticle (Priority priority, int id);
    }
}

