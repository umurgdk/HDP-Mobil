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

        public static List<OrganizationMenuItem> GetMenu ()
        {
            var organizations = new List<OrganizationMenuItem> ();
            organizations.Add (new OrganizationMenuItem {
                Name = "Parti Tüzüğü",
                Order = 0,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Tüzüğü",
                    Document = "PartiTuzugu.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Parti Programı",
                Order = 1,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Programı",
                    Document = "PartiProgrami.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Eş Başkanlar",
                Order = 2,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Eş Başkanlar",
                    Document = "EsBaskanlar.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Merkez Yürütme Kurulu",
                Order = 3,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Merkez Yürütme Kurulu",
                    Document = "MerkezYurutmeKurulu.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Parti Meclisi",
                Order = 5,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Meclisi",
                    Document = "PartiMeclisi.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Danışma Kurulu",
                Order = 6,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Danışma Kurulu",
                    Document = "DanismaKurulu.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Uzlaştırma Kurulu",
                Order = 7,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Uzlaştırma Kurulu",
                    Document = "UzlastirmaKurulu.md"
                }
            });

            organizations.Add (new OrganizationMenuItem {
                Name = "Kurucular",
                Order = 8,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Kurucular",
                    Document = "Kurucular.md"
                }
            });

            return organizations;
        }
    }

}

