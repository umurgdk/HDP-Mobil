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

        private DateTime time;
        public DateTime Time {
            get { return this.time; }
            set { this.RaiseAndSetIfChanged (ref this.time, value); }
        }

        private string location;
        public string Location {
            get { return this.location; }
            set { this.RaiseAndSetIfChanged (ref this.location, value); }
        }

        readonly ObservableAsPropertyHelper<string> dateText;
        public string DateText {
            get { return dateText.Value; }
        }

        readonly ObservableAsPropertyHelper<string> timeText;
        public string TimeText {
            get { return timeText.Value; }
        }

        public Event Model { get; set; }

        public EventViewModel ()
        {
            this.WhenAnyValue (x => x.Time)
                .Select (x => x.ToString ("dd MMMM yyyy"))
                .ToProperty (this, x => x.DateText, out dateText);

            this.WhenAnyValue (x => x.Time)
                .Select (x => x.ToString ("HH:mm"))
                .ToProperty (this, x => x.TimeText, out timeText);
        }
	}
}

