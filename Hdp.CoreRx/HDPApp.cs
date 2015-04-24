using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

using ReactiveUI;
using Splat;
using Akavache;
using Fusillade;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

using Hdp.CoreRx.Models;
using Hdp.CoreRx.Services;
using Hdp.CoreRx.Extensions;
using System.Threading.Tasks;
using Hdp.CoreRx.Helpers;

namespace Hdp.CoreRx
{
    public class HDPApp
    {
        public AppState State { get; private set; }

        ApiService _apiService;
        NewsService _newsService;
        EventsService _eventsService;
        ElectionArticlesService _electionArticlesService;

        IBlobCache _cache;

        public IReactiveCommand<List<Article>> FetchNewArticles { get; private set; }
        public IReactiveCommand<List<Article>> LoadMoreArticles { get; private set; }

        public IReactiveCommand<List<ElectionArticle>> FetchNewElectionArticles { get; private set; } 
        public IReactiveCommand<List<ElectionArticle>> LoadMoreElectionArticles { get; private set; }

        public IReactiveCommand<List<Event>> FetchNewEvents { get; private set; }
        public IReactiveCommand<List<Event>> LoadMoreEvents { get; private set; }

        public HDPApp(DeviceType deviceType)
        {
            RxApp.DefaultExceptionHandler = Observer.Create ((Exception e) => {
                System.Diagnostics.Debug.WriteLine(e.Message); 
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            });

            BlobCache.ApplicationName = "HDP";
            _cache = BlobCache.LocalMachine;

            JsonConvert.DefaultSettings = 
                () => new JsonSerializerSettings() { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = {new StringEnumConverter()}
            };

            _apiService = new ApiService (deviceType: deviceType);
            _newsService = new NewsService (_apiService);
            _eventsService = new EventsService (_apiService);
            _electionArticlesService = new ElectionArticlesService (_apiService);

            Locator.CurrentMutable.RegisterConstant (this, typeof(HDPApp));

            State = new AppState ();

            ImplementCommands ();
        }

        private void ImplementCommands ()
        {
            FetchNewArticles = ReactiveCommand.CreateAsyncTask (async param => {
                var priority = param == null ? Priority.UserInitiated : (Priority)param;

                if (State.Articles.Count > 0)
                {
                    var latestArticle = State.Articles.OrderByDescending(article => article.CreatedAt).First();
                    return await _newsService.GetArticlesAfterAsync(latestArticle, (Priority)priority);
                }
                else
                {
                    return await _newsService.GetRemoteArticlesAsync((Priority)priority);
                }
            });
            
            FetchNewArticles.Subscribe (async newArticles => {
                State.Articles.InsertRange(0, newArticles);

                var articlesKeyValue = newArticles.ToDictionary(x => "article-" + x.Id.ToString());

                foreach (var pair in articlesKeyValue) {
                    await _cache.InsertObject<Article> (pair.Key, pair.Value);
                }
            });

            FetchNewElectionArticles = ReactiveCommand.CreateAsyncTask (async param => {
                var priority = param == null ? Priority.UserInitiated : (Priority)param;

                if (State.ElectionArticles.Count > 0)
                {
                    var latestArticle = State.ElectionArticles.OrderByDescending(article => article.CreatedAt).First();
                    return await _electionArticlesService.GetElectionArticlesAfterAsync(latestArticle, (Priority)priority);
                }
                else
                {
                    return await _electionArticlesService.GetRemoteElectionArticlesAsync((Priority)priority);
                }
            });

            FetchNewElectionArticles.Subscribe (async newArticles => {
                State.ElectionArticles.InsertRange(0, newArticles);

                var articlesKeyValue = newArticles.ToDictionary(x => "election-article-" + x.Id.ToString());

                foreach (var pair in articlesKeyValue) {
                    await _cache.InsertObject<ElectionArticle> (pair.Key, pair.Value);
                }
            });

            FetchNewEvents = ReactiveCommand.CreateAsyncTask (async param => {
                var priority = param == null ? Priority.UserInitiated : (Priority)param;

                if (State.Events.Count > 0)
                {
                    var latestEvent = State.Events.OrderByDescending(ev => ev.CreatedAt).First();
                    return await _eventsService.GetEventsAfterAsync(latestEvent, (Priority)priority);
                }
                else
                {
                    return await _eventsService.GetRemoteEventsAsync((Priority)priority);
                }
            });

            FetchNewEvents.Subscribe (async newEvents => {
                State.Events.InsertRange(0, newEvents);

                var eventsKeyValue = newEvents.ToDictionary(x => "event-" + x.Id.ToString());

                foreach (var pair in eventsKeyValue) {
                    await _cache.InsertObject<Event>(pair.Key, pair.Value);
                }
            });
        }

        public async void Bootstrap()
        {
            #if DEBUG
//            await BlobCache.LocalMachine.InvalidateAll ();
//            await BlobCache.LocalMachine.Vacuum ();
            #endif

            // Fill Articles
            try {
                var articles = await _cache.GetAllObjects<Article> ();
                State.Articles.InsertRange(0, articles);
            } catch (Exception _) {
                
            } finally {
                FetchNewArticles.Execute (Priority.Background);
            }

            try {
                var electionArticles = await _cache.GetAllObjects<ElectionArticle> ();
                State.ElectionArticles.InsertRange(0, electionArticles);
            } catch (Exception _) {
                
            } finally {
                FetchNewElectionArticles.Execute (Priority.UserInitiated);
            }

            try {
                var events = await _cache.GetAllObjects<Event>();
                State.Events.InsertRange(0, events);
            } catch (Exception _) {
                
            } finally {
                FetchNewEvents.Execute (Priority.Background);
            }

            State.OrganizationMenuItems.AddRange (OrganizationMenuItem.GetMenu ());
        }
    }

    public enum DeviceType
    {
        ios,
        ios2x,
        ios3x
    }
}

