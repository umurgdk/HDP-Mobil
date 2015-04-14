using System;
using ReactiveUI;
using System.Reactive;
using Splat;
using Akavache;
using Hdp.CoreRx.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Hdp.CoreRx
{
    public class HDPApp
    {
        public HDPApp(DeviceType deviceType)
        {
            RxApp.DefaultExceptionHandler = Observer.Create ((Exception e) => {
                System.Diagnostics.Debug.WriteLine(e.Message); 
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            });

            BlobCache.ApplicationName = "HDP";
            BlobCache.LocalMachine.InvalidateAll ();
            BlobCache.LocalMachine.Vacuum ();

            JsonConvert.DefaultSettings = 
                () => new JsonSerializerSettings() { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = {new StringEnumConverter()}
            };

            var apiService = new ApiService (deviceType: deviceType);
            var newsService = new NewsService (apiService);
            var eventsService = new EventsService (apiService);
            var electionArticlesService = new ElectionArticlesService (apiService);

            Locator.CurrentMutable.RegisterConstant (apiService, typeof(IApiService));
            Locator.CurrentMutable.RegisterConstant (newsService, typeof(INewsService));
            Locator.CurrentMutable.RegisterConstant (eventsService, typeof(IEventsService));
            Locator.CurrentMutable.RegisterConstant (electionArticlesService, typeof(IElectionArticlesService));
        }
    }

    public enum DeviceType
    {
        ios,
        ios2x,
        ios3x
    }
}

