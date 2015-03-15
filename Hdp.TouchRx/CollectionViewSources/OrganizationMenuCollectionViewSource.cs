using System;
using ReactiveUI;
using Hdp.CoreRx.ViewModels.Organization;
using UIKit;
using Hdp.TouchRx.ViewControllers.Organization;

namespace Hdp.TouchRx.CollectionViewSources
{
    public class OrganizationMenuCollectionViewSource : BaseCollectionViewSource<OrganizationMenuItemViewModel>
    {
        public OrganizationMenuCollectionViewSource (UICollectionView collectionView, IReactiveNotifyCollectionChanged<OrganizationMenuItemViewModel> collection)
            : base (collectionView, collection, OrganizationMenuItemViewCell.Key, null)
        {
            collectionView.RegisterNibForCell (OrganizationMenuItemViewCell.Nib, OrganizationMenuItemViewCell.Key);
        }

        public OrganizationMenuCollectionViewSource (UICollectionView collectionView) 
            : base (collectionView)
        {
            collectionView.RegisterNibForCell (OrganizationMenuItemViewCell.Nib, OrganizationMenuItemViewCell.Key);
        }
    }
}

