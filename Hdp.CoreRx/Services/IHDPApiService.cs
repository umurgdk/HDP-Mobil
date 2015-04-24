using System;
using System.Threading.Tasks;
using Hdp.CoreRx.Models;
using System.Collections.Generic;
using Refit;

namespace Hdp.CoreRx.Services
{
    [Headers("Accept: application/json")]
    public interface IHDPApiService
    {
        // Articles
        [Get("/articles?{deviceType}=1")]
        Task<List<Article>> GetArticles (DeviceType deviceType = DeviceType.ios);

        [Get("/articles?after={after}&{deviceType}=1")]
        Task<List<Article>> GetArticlesAfter (int after, DeviceType deviceType = DeviceType.ios);

        [Get("/articles?before={before}&{deviceType}=1")]
        Task<List<Article>> GetArticlesBefore (int before, DeviceType deviceType = DeviceType.ios);

        // Events
        [Get("/events?{deviceType}=1")]
        Task<List<Event>> GetEvents (DeviceType deviceType = DeviceType.ios);

        [Get("/events?after={after}&{deviceType}=1")]
        Task<List<Event>> GetEventsAfter (int after, DeviceType deviceType = DeviceType.ios);

        [Get("/events?before={before}&{deviceType}=1")]
        Task<List<Event>> GetEventsBefore (int before, DeviceType deviceType = DeviceType.ios);

        // Election Articles
        [Get("/election_articles?{deviceType}=1")]
        Task<List<ElectionArticle>> GetElectionArticles (DeviceType deviceType = DeviceType.ios);

        [Get("/election_articles?after={after}&{deviceType}=1")]
        Task<List<ElectionArticle>> GetElectionArticlesAfter (int after, DeviceType deviceType = DeviceType.ios);

        [Get("/election_articles?before={before}&{deviceType}=1")]
        Task<List<ElectionArticle>> GetElectionArticlesBefore (int before, DeviceType deviceType = DeviceType.ios);
    }
}

