using System;
using ReactiveUI;
using Hdp.CoreRx.Models;
using Hdp.CoreRx.Services;
using System.Collections.Generic;
using Fusillade;
using System.Reactive.Linq;

namespace Hdp.CoreRx.ViewModels.Events
{
    public class EventsViewModel : BaseViewModel, ILoadingViewModel
    {
        public IReactiveDerivedList<EventItemViewModel> EventItems { get; private set; }
        public IReactiveCommand FetchNewEvents { get; private set; }

        private ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading {
            get { return this._isLoading.Value; }
        }

        public EventsViewModel (HDPApp _app)
        {
            Title = "Etkinlikler";

            EventItems = _app.State.Events.CreateDerivedCollection (
                x => new EventItemViewModel(x),
                orderer: (x, y) => y.Model.Time.CompareTo(x.Model.Time));

            this.WhenAnyValue (x => x.EventItems.Count)
                .Select (x => x == 0)
                .ToProperty (this, x => x.IsLoading, out _isLoading, true);

            FetchNewEvents = ReactiveCommand.CreateCombined (
                _app.FetchNewEvents.CanExecuteObservable, 
                _app.FetchNewEvents);
        }
    }
}

