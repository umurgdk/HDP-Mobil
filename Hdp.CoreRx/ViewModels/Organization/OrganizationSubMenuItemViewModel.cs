using System;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationSubMenuItemViewModel : OrganizationMenuItemViewModel
    {
        public OrganizationSubMenuItemViewModel (OrganizationMenuItem model, Action<OrganizationMenuItemViewModel> gotoCommand)
            : base (model, gotoCommand)
        {
        }
    }
}

