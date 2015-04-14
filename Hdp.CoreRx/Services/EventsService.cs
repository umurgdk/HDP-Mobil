using System;
using Fusillade;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hdp.CoreRx.Models;
using Akavache;
using Connectivity.Plugin;
using Polly;
using System.Net;

namespace Hdp.CoreRx.Services
{
    public class EventsService : IEventsService
    {
        private readonly IApiService _apiService;

        public EventsService (IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Event>> GetEvents (Priority priority)
        {
            var cache = BlobCache.LocalMachine;
            var cachedEvents = cache.GetAndFetchLatest ("events", () => GetRemoteEventsAsync (priority));

            return await cachedEvents.FirstOrDefaultAsync ();
        }

        private async Task<List<Event>> GetRemoteEventsAsync (Priority priority)
        {
            List<Event> events = null;
            Task<List<Event>> getEventsTask;

            switch (priority) {
            case Priority.Background:
                getEventsTask = _apiService.Background.GetEvents (ApiService.Device);
                break;
            case Priority.UserInitiated:
                getEventsTask = _apiService.UserInitiated.GetEvents (ApiService.Device);
                break;
            case Priority.Speculative:
                getEventsTask = _apiService.Speculative.GetEvents (ApiService.Device);
                break;
            default:
                getEventsTask = _apiService.Background.GetEvents (ApiService.Device);
                break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                events = await Policy
                    .Handle<WebException> ()
                    .WaitAndRetryAsync (5, retryAttempt => TimeSpan.FromSeconds (Math.Pow (2, retryAttempt)))
                    .ExecuteAsync (async () => await getEventsTask);
            }

            return events;
        }
    }

    public interface IEventsService
    {
        Task<List<Event>> GetEvents (Priority priority);
    }
}

