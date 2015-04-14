using System;
using ReactiveUI;
using Hdp.CoreRx.Models;
using Hdp.CoreRx.Services;
using System.Collections.Generic;
using Fusillade;

namespace Hdp.CoreRx.ViewModels.Events
{
    public class EventsViewModel : BaseViewModel
    {
        public ReactiveList<Event> Events { get; protected set; } = new ReactiveList<Event>();
        public IReactiveDerivedList<EventItemViewModel> EventItems;

        private readonly IEventsService _eventsService;

        public IReactiveCommand<List<Event>> LoadCommand;

        public EventsViewModel (IEventsService eventsService)
        {
            _eventsService = eventsService;

            Title = "Etkinlikler";

            var gotoCommand = new Action<EventItemViewModel> (x => {
                var vm = this.CreateViewModel<EventViewModel>();
                vm.EventTitle = x.Model.Title;
                vm.Time = x.Model.Time;
                vm.Location = x.Model.Location;
                
                NavigateTo(vm);
            });

            EventItems = Events.CreateDerivedCollection (
                x => new EventItemViewModel(x, gotoCommand),
                orderer: (x, y) => y.Model.Time.CompareTo(x.Model.Time));

            LoadCommand = ReactiveCommand.CreateAsyncTask (async _ => {
                return await _eventsService.GetEvents(Priority.UserInitiated);
            });

            LoadCommand.Subscribe (events => {
                Events.Reset();
                Events.AddRange(events);
            });
        }
    }
}

