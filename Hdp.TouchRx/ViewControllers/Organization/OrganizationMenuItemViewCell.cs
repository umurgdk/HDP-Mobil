using System;
using System.Linq;
using System.Reactive.Linq;

using Foundation;
using UIKit;
using ReactiveUI;
using Hdp.CoreRx.ViewModels.Organization;

namespace Hdp.TouchRx.ViewControllers.Organization
{
    public partial class OrganizationMenuItemViewCell : ReactiveCollectionViewCell, IViewFor<OrganizationMenuItemViewModel>
    {
        public static readonly UINib Nib = UINib.FromName ("OrganizationMenuItemViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString ("OrganizationMenuItemViewCell");

        public OrganizationMenuItemViewCell (IntPtr handle) : base (handle)
        {
        }

        public static OrganizationMenuItemViewCell Create ()
        {
            return (OrganizationMenuItemViewCell)Nib.Instantiate (null, null) [0];
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();

            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    iconView.Image = new UIImage(x.ImageName);
                    nameLabel.Text = x.Name;
                });
        }

        private OrganizationMenuItemViewModel _viewModel;
        public OrganizationMenuItemViewModel ViewModel 
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel 
        {
            get { return _viewModel; }
            set { ViewModel = (OrganizationMenuItemViewModel)value; }
        }
    }
}

