using System;
using Hdp.CoreRx.ViewModels.Organization;
using Foundation;
using UIKit;

namespace Hdp.TouchRx.TableViewSources
{
    public class OrganizationSubmenuTableViewSource : BaseTableViewSource<OrganizationMenuItemViewModel>
    {
        public static readonly string CellIdentifier = "SubMenuMenuItemCell";

        public OrganizationSubmenuTableViewSource (UIKit.UITableView tableView, nfloat sizeHint) 
            : base (tableView, sizeHint)
        {
        }
        

        public OrganizationSubmenuTableViewSource (UIKit.UITableView tableView, ReactiveUI.IReactiveNotifyCollectionChanged<OrganizationMenuItemViewModel> collection, nfloat sizeHint, Action<UIKit.UITableViewCell> initializeCellAction = null) 
            : base (tableView, collection, new NSString(CellIdentifier), sizeHint, initializeCellAction)
        {
        }

        public override UIKit.UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (CellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
            }

            var vm = ItemAt (indexPath) as OrganizationMenuItemViewModel;
            cell.TextLabel.Text = vm.Name;

            return cell;
        }
    }
}

