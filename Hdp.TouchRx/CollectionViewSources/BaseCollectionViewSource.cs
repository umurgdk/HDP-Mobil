using System;
using ReactiveUI;
using UIKit;
using Foundation;
using Hdp.CoreRx.ViewModels;
using Hdp.CoreRx.Extensions;

namespace Hdp.TouchRx.CollectionViewSources
{
    public class BaseCollectionViewSource<TViewModel> : ReactiveCollectionViewSource<TViewModel>
    {
        public BaseCollectionViewSource (UICollectionView collectionView)
            : base(collectionView)
        {
        }

        protected BaseCollectionViewSource(UICollectionView collectionView, 
            IReactiveNotifyCollectionChanged<TViewModel> collection,
            NSString cellKey,
            Action<UICollectionViewCell> initializeCellAction)
            : base(collectionView, collection, cellKey, initializeCellAction)
        {

        }

        public override nint NumberOfSections (UICollectionView collectionView)
        {
            if (Data == null || Data.Count == 0)
                return 0;
            return base.NumberOfSections(collectionView);
        }

        public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
        {
            var item = ItemAt (indexPath) as ICanGoToViewModel;

            if (item != null) {
                item.GoToCommand.ExecuteIfCan ();
            }

            base.ItemSelected (collectionView, indexPath);
        }
    }
}

