using System;
using System.Collections.Generic;

namespace Hdp.CoreRx.Models
{
    public enum MenuType
    {
        View,
        SubMenu
    }

    public class OrganizationMenuItem
    {
        public string ImageName {get;set;} = "BookIcon";
        public string Name {get;set;}

        public int Order {get;set;}

        public MenuType MenuType {get;set;}

        public List<OrganizationMenuItem> SubMenuItems = new List<OrganizationMenuItem>();
        public OrganizationPage Page;
    }
}

