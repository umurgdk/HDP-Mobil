using System;
using System.Reactive.Linq;

using Foundation;
using UIKit;

using ReactiveUI;
using Hdp.CoreRx.ViewModels.Events;

namespace Hdp.TouchRx.ViewControllers.Events
{
    public partial class EventItemViewCell : ReactiveTableViewCell, IViewFor<EventItemViewModel>
    {
        public static readonly UINib Nib = UINib.FromName ("EventItemViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("EventItemViewCell");

        public static readonly nfloat EstimatedHeight = 85.0f;

        public EventItemViewCell (IntPtr handle) : base (handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        public static EventItemViewCell Create ()
        {
            return (EventItemViewCell)Nib.Instantiate (null, null) [0];
        }

        public nfloat GetHeight ()
        {
            return placeLabel.Frame.Bottom + 16.0f;
        }

//        public override void WillMoveToSuperview (UIView newsuper)
//        {
//            eventTitleLabel.PreferredMaxLayoutWidth = newsuper.Frame.Width;
//            base.WillMoveToSuperview (newsuper);
//        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            iconView.Image = new UIImage ("CalendarIconSm");

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    eventTitleLabel.Text = x.EventTitle;
                    dateLabel.Text = x.DateText;
                    SetNeedsLayout();
                    LayoutIfNeeded();
                });
        }

        private EventItemViewModel _viewModel;
        public EventItemViewModel ViewModel 
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel 
        {
            get { return _viewModel; }
            set { ViewModel = (EventItemViewModel)value; }
        }
    }
}

