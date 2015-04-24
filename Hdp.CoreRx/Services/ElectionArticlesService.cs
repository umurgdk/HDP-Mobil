using System;
using Hdp.CoreRx.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fusillade;
using Akavache;
using System.Linq;
using System.Reactive.Linq;
using Connectivity.Plugin;
using Polly;
using System.Net;
using Hdp.CoreRx.Helpers;
using Hdp.CoreRx.Extensions;

namespace Hdp.CoreRx.Services
{
    public class ElectionArticlesService : IElectionArticlesService
    {
        private readonly IApiService _apiService;

        public ElectionArticlesService (IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ElectionArticle>> GetElectionArticles (Priority priority)
        {
            var cache = BlobCache.LocalMachine;
            var cachedArticles = cache.GetAndFetchLatest ("election-articles", () => GetRemoteElectionArticlesAsync (priority));

            return await cachedArticles.FirstOrDefaultAsync ();
        }

        public async Task<List<ElectionArticle>> GetElectionArticlesAfterAsync (ElectionArticle latestArticle, Priority priority)
        {
            List<ElectionArticle> articles = null;
            Task<List<ElectionArticle>> getArticlesTask;

            var timestamp = latestArticle.CreatedAt.ToUnixTimestamp ();

            switch (priority) {
            case Priority.Background:
                getArticlesTask = _apiService.Background.GetElectionArticlesAfter (timestamp, ApiService.Device);
                break;
            case Priority.UserInitiated:
                getArticlesTask = _apiService.UserInitiated.GetElectionArticlesAfter (timestamp, ApiService.Device);
                break;
            case Priority.Speculative:
                getArticlesTask = _apiService.Speculative.GetElectionArticlesAfter (timestamp, ApiService.Device);
                break;
            default:
                getArticlesTask = _apiService.UserInitiated.GetElectionArticlesAfter (timestamp, ApiService.Device);
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

        public async Task<List<ElectionArticle>> GetRemoteElectionArticlesAsync (Priority priority)
        {
            List<ElectionArticle> articles = null;
            Task<List<ElectionArticle>> getArticlesTask;

            switch (priority) {
            case Priority.Background:
                getArticlesTask = _apiService.Background.GetElectionArticles (ApiService.Device);
                break;
            case Priority.UserInitiated:
                getArticlesTask = _apiService.UserInitiated.GetElectionArticles (ApiService.Device);
                break;
            case Priority.Speculative:
                getArticlesTask = _apiService.Speculative.GetElectionArticles (ApiService.Device);
                break;
            default:
                getArticlesTask = _apiService.UserInitiated.GetElectionArticles (ApiService.Device);
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
                if (article.Type == ElectionArticle.MediaType.Video)
                {
                    article.VideoImageUrl = YoutubeHelper.GetThumbnailUrl(ApiService.Device, article.VideoUrl);
                    article.VideoId = YoutubeHelper.GetVideoId(article.VideoUrl);
                }

                else if (article.Type == ElectionArticle.MediaType.Image)
                {
                    article.ImageUrl = ApiService.ApiBaseAddress + article.ImageUrl;
                }

                return article;
            }).ToList();
        }
    }

    public interface IElectionArticlesService
    {
        Task<List<ElectionArticle>> GetElectionArticles(Priority priority);
    }
}

