using System;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using Hdp.CoreRx.Models;
using System.Collections.Generic;

namespace Hdp.CoreRx.ViewModels.Organization
{
    public class OrganizationMenuItemViewModel : BaseViewModel, ICanGoToViewModel
    {
        private string imageName;
        public string ImageName {
            get { return this.imageName; }
            set { this.RaiseAndSetIfChanged (ref this.imageName, value); }
        }

        private string name;
        public string Name {
            get { return this.name; }
            set { this.RaiseAndSetIfChanged (ref this.name, value); }
        }

        private int order;
        public int Order
        {
            get { return this.order; }
            set { this.RaiseAndSetIfChanged(ref this.order, value); }
        }

        public IReactiveDerivedList<OrganizationMenuItemViewModel> SubMenuItems;

        public OrganizationMenuItem Model { get; private set; }
        public IReactiveCommand<object> GoToCommand { get; private set; }

        public OrganizationMenuItemViewModel (OrganizationMenuItem model, Action<OrganizationMenuItemViewModel> gotoCommand)
        {
            Model = model;

            ImageName = model.ImageName;
            Name = model.Name;
            Title = model.Name;

            SubMenuItems = model.SubMenuItems.CreateDerivedCollection (x => new OrganizationSubMenuItemViewModel(x, gotoCommand));

            GoToCommand = ReactiveCommand.Create ();
            GoToCommand.Subscribe (x => gotoCommand (this));
        }
    }
}

