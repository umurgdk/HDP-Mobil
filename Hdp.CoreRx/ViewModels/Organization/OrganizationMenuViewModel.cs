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
                // TODO: Implement organization menu item gotoCommand
                if (x.Model.MenuType == MenuType.View) {
                    var page = x.Model.Page;

                    var vm = new OrganizationPageViewModel();
                    vm.Title = page.Title;
                    vm.Body = page.Body;

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
                Name = "Parti Tuzugu",
                Order = 0,
                MenuType = MenuType.View,
                Page = new OrganizationPage {
                    Title = "Parti Tuzugu",
                    Body = "Lorem ipsum..."
                }
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Parti Programi",
                Order = 1
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Temel Metinler",
                Order = 2
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Komisyonlar",
                Order = 3
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Meclisler",
                Order = 4
            });

            Organizations.Add (new OrganizationMenuItem {
                Name = "Yurutme",
                Order = 5,
                MenuType = MenuType.SubMenu,
                SubMenuItems = new List<OrganizationMenuItem> {
                    new OrganizationMenuItem {
                        Name = "Esbaskanlar",
                        Order = 0,
                        MenuType = MenuType.SubMenu,
                        SubMenuItems = new List<OrganizationMenuItem> {
                            new OrganizationMenuItem {
                                Name = "Wohoooo!"
                            }
                        }
                    },

                    new OrganizationMenuItem {
                        Name = "Merkez Yurutme Kurulu",
                        Order = 1,
                        MenuType = MenuType.View,
                        Page = new OrganizationPage { Title = "Merkez Yurutme Kurulu", Body = "Merkez Yurutme Kurulu icerik" }
                    },

                    new OrganizationMenuItem {
                        Name = "Parti Meclisi",
                        Order = 2,
                        MenuType = MenuType.View,
                        Page = new OrganizationPage { Title = "Parti Meclisi", Body = "Parti Meclisi icerik" }
                    },

                    new OrganizationMenuItem {
                        Name = "Danisma Kurulu",
                        Order = 3,
                        MenuType = MenuType.View,
                        Page = new OrganizationPage { Title = "Danisma Kurulu", Body = "Danisma Kurulu icerik" }
                    },
                }
            });
        }
    }
}

