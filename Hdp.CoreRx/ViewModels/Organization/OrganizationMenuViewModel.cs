using System;
using System.Collections.Generic;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationMenuViewModel : BaseViewModel
    {
        public IReactiveDerivedList<OrganizationMenuItemViewModel> OrganizationItems;

        public OrganizationMenuViewModel (HDPApp _app)
        {
            Title = "Parti";

            Action<OrganizationMenuItemViewModel> gotoCommand = null;
            gotoCommand = new Action<OrganizationMenuItemViewModel> (x => {
                if (x.Model.MenuType == MenuType.View) {
                    var page = x.Model.Page;

                    var vm = new OrganizationPageViewModel();
                    vm.Title = page.Title;
                    vm.Document = page.Document;

                    NavigateTo(vm);
                }

                else if (x.Model.MenuType == MenuType.SubMenu)
                {
                    var subMenuItemVM = new OrganizationSubMenuItemViewModel(x.Model, gotoCommand);
                    subMenuItemVM.Title = x.Title;
                    NavigateTo(subMenuItemVM);
                }
            });

            OrganizationItems = _app.State.OrganizationMenuItems.CreateDerivedCollection (
                x => new OrganizationMenuItemViewModel (x, gotoCommand));
        }
    }
}

