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
using System.Reactive.Subjects;

namespace Hdp.CoreRx
{
    public class HDPApp
    {
        public AppState State { get; private set; }

        ApiService _apiService;
        NewsService _newsService;
        EventsService _eventsService;
        ElectionArticlesService _electionArticlesService;

        ContentRepository _repository;

        IBlobCache _cache;

        public IReactiveCommand<List<Article>> FetchNewArticles { get; private set; }
        public IReactiveCommand<List<ElectionArticle>> FetchNewElectionArticles { get; private set; }
        public IReactiveCommand<List<Event>> FetchNewEvents { get; private set; }


        public Subject<string> UserCity = new Subject<string>();

        public HDPApp(DeviceType deviceType, IBlobCache cache)
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
                Converters = {new MediaTypeConverter(), new StringEnumConverter()}
            };

            _apiService = new ApiService (deviceType: deviceType);
            _newsService = new NewsService (_apiService);
            _eventsService = new EventsService (_apiService);
            _electionArticlesService = new ElectionArticlesService (_apiService);

            Locator.CurrentMutable.RegisterConstant (this, typeof(HDPApp));

            State = new AppState ();

            UserCity.Subscribe (city => {
                System.Diagnostics.Debug.WriteLine("Current city is: {0}", city); 
            });

            _repository = new ContentRepository (_newsService, _electionArticlesService, _eventsService, cache);

            ImplementCommands ();
        }

        private void ImplementCommands ()
        {
            FetchNewArticles = ReactiveCommand.CreateAsyncTask (async param => {
                var priority = param == null ? Priority.UserInitiated : (Priority)param;

                var newAndUpdatedArticles = await _repository.FetchNewArticles(State.Articles, priority);
                State.Articles.InsertRange(0, newAndUpdatedArticles.Item1);

                for (int i = 0; i < newAndUpdatedArticles.Item2.Count; i++) {
                    State.Articles[i] = newAndUpdatedArticles.Item2[i];
                }

                return newAndUpdatedArticles.Item1;
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

            // Load Articles
            try {
                var articles = await _cache.GetAllObjects<Article> ();
                State.Articles.InsertRange(0, articles);
            } catch (Exception _) {
                
            } finally {
                FetchNewArticles.Execute (Priority.Background);
            }

            // Load Election Articles
            try {
                var electionArticles = await _cache.GetAllObjects<ElectionArticle> ();
                State.ElectionArticles.InsertRange(0, electionArticles);
            } catch (Exception _) {
                
            } finally {
                FetchNewElectionArticles.Execute (Priority.UserInitiated);
            }

            // Load Events
            try {
                var events = await _cache.GetAllObjects<Event>();
                State.Events.InsertRange(0, events);
            } catch (Exception _) {
                
            } finally {
                FetchNewEvents.Execute (Priority.Background);
            }

            // Load hardcoded Organization Pages
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

