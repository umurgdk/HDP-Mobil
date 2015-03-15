using System;
using Hdp.CoreRx.ViewModels.Organization;
using Hdp.TouchRx.TableViewSources;
using ReactiveUI;
using Foundation;

namespace Hdp.TouchRx.ViewControllers.Organization
{
    public class OrganizationSubmenuViewController : BaseTableViewController<OrganizationSubMenuItemViewModel>
    {
        public OrganizationSubmenuViewController ()
            : this(UIKit.UITableViewStyle.Plain)
        {
        }

        public OrganizationSubmenuViewController (UIKit.UITableViewStyle style) : base (style)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            Title = ViewModel.Title;

            var topItem = NavigationController.NavigationBar.TopItem;
            topItem.Title = "Parti";

            TableView.Source = new OrganizationSubmenuTableViewSource (TableView, ViewModel.SubMenuItems, 44.0f);
        }
    }
}

