// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hdp.TouchRx.Views.News
{
	[Register ("ArticleItemViewCell")]
	partial class ArticleItemViewCell
	{
		[Outlet]
		UIKit.UILabel categoryLabel { get; set; }

		[Outlet]
		UIKit.UILabel dateLabel { get; set; }

		[Outlet]
		UIKit.UIImageView thumbnailImageView { get; set; }

		[Outlet]
		UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (categoryLabel != null) {
				categoryLabel.Dispose ();
				categoryLabel = null;
			}

			if (dateLabel != null) {
				dateLabel.Dispose ();
				dateLabel = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (thumbnailImageView != null) {
				thumbnailImageView.Dispose ();
				thumbnailImageView = null;
			}
		}
	}
}
