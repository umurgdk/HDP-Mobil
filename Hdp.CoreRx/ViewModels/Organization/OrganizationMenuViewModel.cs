using System;
using System.Collections.Generic;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationMenuViewModel : BaseViewModel
    {
        public ReactiveList<OrganizationMenuItem> Organizations { get; protected set; } = new ReactiveList<OrganizationMenuItem>();
        public IReactiveDerivedList<OrganizationMenuItemViewModel> OrganizationItems;

        public OrganizationMenuViewModel ()
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

            OrganizationItems = Organizations.CreateDerivedCollection (
                x => new OrganizationMenuItemViewModel (x, gotoCommand));

            Organizations.Add (new OrganizationMenuItem {
                Name = "Parti Tüzüğü",
                Order = 0,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Tüzüğü",
                    Document = "PartiTuzugu.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Parti Programı",
                Order = 1,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Programı",
                    Document = "PartiProgrami.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Eş Başkanlar",
                Order = 2,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Eş Başkanlar",
                    Document = "EsBaskanlar.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Merkez Yürütme Kurulu",
                Order = 3,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Merkez Yürütme Kurulu",
                    Document = "MerkezYurutmeKurulu.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Parti Meclisi",
                Order = 5,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Meclisi",
                    Document = "PartiMeclisi.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Danışma Kurulu",
                Order = 6,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Danışma Kurulu",
                    Document = "DanismaKurulu.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Uzlaştırma Kurulu",
                Order = 7,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Uzlaştırma Kurulu",
                    Document = "UzlastirmaKurulu.md"
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Kurucular",
                Order = 8,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Kurucular",
                    Document = "Kurucular.md"
                }
            });
        }
    }
}

