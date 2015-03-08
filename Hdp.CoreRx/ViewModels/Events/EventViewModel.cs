using System;
using System.Reactive.Linq;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels.Events
{
    public class EventViewModel : BaseViewModel
	{
        private string eventTitle;
        public string EventTitle {
            get { return this.eventTitle; }
            set { this.RaiseAndSetIfChanged (ref this.eventTitle, value); }
        }

        private DateTime date;
        public DateTime Date {
            get { return this.date; }
            set { this.RaiseAndSetIfChanged (ref this.date, value); }
        }

        private string place;
        public string Place {
            get { return this.place; }
            set { this.RaiseAndSetIfChanged (ref this.place, value); }
        }

        readonly ObservableAsPropertyHelper<string> dateText;
        public string DateText {
            get { return dateText.Value; }
        }

        public Event Model { get; set; }

        public EventViewModel ()
        {
            this.WhenAnyValue (x => x.Date)
                .Select (x => x.ToString ("dd MMMM yyyy"))
                .ToProperty (this, x => x.DateText, out dateText);
        }
	}
}

