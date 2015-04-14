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
        [Get("/articles?{deviceType}=1")]
        Task<List<Article>> GetArticles(DeviceType deviceType = DeviceType.ios);

        [Get("/articles/{id}?{deviceType}=1")]
        Task<Article> GetArticleById(int id, DeviceType deviceType = DeviceType.ios);

        [Get("/events?{deviceType}=1")]
        Task<List<Event>> GetEvents (DeviceType deviceType = DeviceType.ios);

        [Get("/election_articles?{deviceType}=1")]
        Task<List<ElectionArticle>> GetElectionArticles (DeviceType deviceType = DeviceType.ios);
    }
}

