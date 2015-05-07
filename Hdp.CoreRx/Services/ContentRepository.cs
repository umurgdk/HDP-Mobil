using System;
using System.Threading.Tasks;
using Hdp.CoreRx.Models;
using Fusillade;
using System.Collections.Generic;
using Akavache;
using System.Linq;
using System.Reactive.Linq;

namespace Hdp.CoreRx.Services
{
    public class ContentRepository
    {
        NewsService _news;
        ElectionArticlesService _electionArticles;
        EventsService _events;

        IBlobCache _cache;

        public ContentRepository (
            NewsService newsService, 
            ElectionArticlesService electionArticlesService, 
            EventsService eventsService,
            IBlobCache cache)
        {
            _news = newsService;
            _electionArticles = electionArticlesService;
            _events = eventsService;

            _cache = cache;
        }

        public async Task<Tuple<List<Article>, List<Article>>> FetchNewArticles (IEnumerable<Article> currentArticles, Priority priority)
        {
            IList<Article> fetchedArticles;

            Article latestArticle = currentArticles.OrderByDescending(article => article.CreatedAt).FirstOrDefault();

            if (latestArticle != null)
            {
                fetchedArticles = await _news.GetArticlesAfterAsync(latestArticle, (Priority)priority);
            }
            else
            {
                fetchedArticles = await _news.GetRemoteArticlesAsync((Priority)priority);
            }

            var articlesKeyValue = fetchedArticles.ToDictionary(x => "article-" + x.Id.ToString());
            await _cache.InsertAllObjects<Article> (articlesKeyValue);

            var newArticles = fetchedArticles.Except (currentArticles).ToList();
            var updatedArticles = fetchedArticles.Intersect (currentArticles).ToList();

            return new Tuple<List<Article>, List<Article>> (newArticles, updatedArticles);
        }
    }
}

