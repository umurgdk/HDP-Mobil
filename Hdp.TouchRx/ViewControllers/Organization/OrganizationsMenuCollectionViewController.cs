using System;
using System.Linq;
using System.Reactive.Linq;

using Foundation;
using UIKit;
using ReactiveUI;
using Hdp.CoreRx.ViewModels.Organization;
using Hdp.TouchRx.CollectionViewSources;
using CoreGraphics;

namespace Hdp.TouchRx.ViewControllers.Organization
{
    public class OrganizationsMenuCollectionViewController : BaseCollectionViewController<OrganizationMenuViewModel>
    {
        public OrganizationsMenuCollectionViewController () : base(DefaultLayout())
        {
            TabBarItem.Image = new UIImage ("PartyIcon");
        }

        private static UICollectionViewLayout DefaultLayout ()
        {
            var layout = new UICollectionViewFlowLayout () {
                ItemSize = new CGSize(160, 150),
                MinimumInteritemSpacing = 0.0f,
                MinimumLineSpacing = 0.0f,
                ScrollDirection = UICollectionViewScrollDirection.Vertical
            };

            return layout;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			
            CollectionView.BackgroundColor = UIColor.White;
            CollectionView.Source = new OrganizationMenuCollectionViewSource (CollectionView, ViewModel.OrganizationItems);
        }
    }
}

