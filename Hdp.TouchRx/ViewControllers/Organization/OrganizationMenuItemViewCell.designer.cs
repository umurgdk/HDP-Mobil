// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hdp.TouchRx.ViewControllers.Organization
{
	[Register ("OrganizationMenuItemViewCell")]
	partial class OrganizationMenuItemViewCell
	{
		[Outlet]
		UIKit.UIImageView iconView { get; set; }

		[Outlet]
		UIKit.UILabel nameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (iconView != null) {
				iconView.Dispose ();
				iconView = null;
			}

			if (nameLabel != null) {
				nameLabel.Dispose ();
				nameLabel = null;
			}
		}
	}
}
