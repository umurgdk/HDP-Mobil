// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hdp.TouchRx.ViewControllers.ElectionArticles
{
	[Register ("ElectionArticleTableViewCell")]
	partial class ElectionArticleTableViewCell
	{
		[Outlet]
		UIKit.UILabel articleDateLabel { get; set; }

		[Outlet]
		UIKit.UIImageView articleImage { get; set; }

		[Outlet]
		UIKit.UILabel articleSummaryLabel { get; set; }

		[Outlet]
		UIKit.UILabel articleTitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView playIcon { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (articleDateLabel != null) {
				articleDateLabel.Dispose ();
				articleDateLabel = null;
			}

			if (articleImage != null) {
				articleImage.Dispose ();
				articleImage = null;
			}

			if (articleSummaryLabel != null) {
				articleSummaryLabel.Dispose ();
				articleSummaryLabel = null;
			}

			if (playIcon != null) {
				playIcon.Dispose ();
				playIcon = null;
			}

			if (articleTitleLabel != null) {
				articleTitleLabel.Dispose ();
				articleTitleLabel = null;
			}
		}
	}
}
