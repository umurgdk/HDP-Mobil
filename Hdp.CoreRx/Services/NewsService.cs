using System;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;

using Fusillade;
using Akavache;
using Connectivity.Plugin;
using Polly;

using Hdp.CoreRx.Models;
using Hdp.CoreRx.Extensions;

namespace Hdp.CoreRx.Services
{
    public class NewsService
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

        public async Task<List<Article>> GetArticlesAfterAsync (Article latestArticle, Priority priority)
        {
            List<Article> articles = null;
            Task<List<Article>> getArticlesTask;

            var timestamp = latestArticle.CreatedAt.ToUnixTimestamp ();

            switch (priority) {
            case Priority.Background:
                getArticlesTask = _apiService.Background.GetArticlesAfter (timestamp, ApiService.Device);
                break;
            case Priority.UserInitiated:
                getArticlesTask = _apiService.UserInitiated.GetArticlesAfter (timestamp, ApiService.Device);
                break;
            case Priority.Speculative:
                getArticlesTask = _apiService.Speculative.GetArticlesAfter (timestamp, ApiService.Device);
                break;
            default:
                getArticlesTask = _apiService.UserInitiated.GetArticlesAfter (timestamp, ApiService.Device);
                break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                articles = await Policy
                    .Handle<WebException> ()
                    .WaitAndRetryAsync (2, retryAttempt => TimeSpan.FromSeconds (Math.Pow (2, retryAttempt)))
                    .ExecuteAsync(async () => await getArticlesTask);
            }

            return articles.Select(article => {
                article.ImageUrl = ApiService.ApiBaseAddress + article.ImageUrl;
                return article;
            }).ToList();
        }

        public async Task<List<Article>> GetRemoteArticlesAsync (Priority priority)
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
    }
}

