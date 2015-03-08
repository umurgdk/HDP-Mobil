// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hdp.TouchRx.ViewControllers.Events
{
	[Register ("EventItemViewCell")]
	partial class EventItemViewCell
	{
		[Outlet]
		UIKit.UILabel dateLabel { get; set; }

		[Outlet]
		UIKit.UILabel eventTitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView iconView { get; set; }

		[Outlet]
		UIKit.UILabel placeLabel { get; set; }

		[Outlet]
		UIKit.UILabel timeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (dateLabel != null) {
				dateLabel.Dispose ();
				dateLabel = null;
			}

			if (eventTitleLabel != null) {
				eventTitleLabel.Dispose ();
				eventTitleLabel = null;
			}

			if (iconView != null) {
				iconView.Dispose ();
				iconView = null;
			}

			if (timeLabel != null) {
				timeLabel.Dispose ();
				timeLabel = null;
			}

			if (placeLabel != null) {
				placeLabel.Dispose ();
				placeLabel = null;
			}
		}
	}
}
