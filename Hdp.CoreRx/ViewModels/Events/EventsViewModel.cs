using System;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels.Events
{
    public class EventsViewModel : BaseViewModel
    {
        public ReactiveList<Event> Events { get; protected set; } = new ReactiveList<Event>();
        public IReactiveDerivedList<EventItemViewModel> EventItems;

        public EventsViewModel ()
        {
            Title = "Etkinlikler";

            var gotoCommand = new Action<EventItemViewModel> (x => {
                var vm = this.CreateViewModel<EventViewModel>();
                vm.EventTitle = x.Model.Title;
                vm.Date = x.Model.Date;
                vm.Place = x.Model.Place;
                
                NavigateTo(vm);
            });

            EventItems = Events.CreateDerivedCollection (
                x => new EventItemViewModel(x, gotoCommand),
                orderer: (x, y) => y.Model.Date.CompareTo(x.Model.Date));

//            for (int i = 0; i < 20; i++) {
//                Articles.Add (Article.Create ("Article " + i.ToString ()));
//            }

            Events.Add(new Event {
                Title = "Antalya Il Kongresi",
                Date = new DateTime(2015, 1, 7),
                Place = "Antalya"
            });

            Events.Add(new Event {
                Title = "Istanbul Il Kongresi",
                Date = new DateTime(2015, 1, 4),
                Place = "Istanbul"
            });

            Events.Add(new Event {
                Title = "Kocaeli Il Kongresi",
                Date = new DateTime(2015, 1, 10),
                Place = "Kocaeli"
            });

            Events.Add (new Event {
                Title = "HDP Eşbaşkanı Ertuğrul Kürkçü, yolsuzlukla ilgili basın toplantısı düzenleyecek",
                Date = new DateTime(2014, 2, 26),
                Place = "HDP Ankara Il Binasi"
            });

            for (int i = 1; i < 30; i++) {
                Events.Add(new Event {
                    Title = "Kocaeli Il Kongresi",
                    Date = new DateTime(2013, 1, i),
                    Place = "Kocaeli"
                });
            }
        }
    }
}

