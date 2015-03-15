using System;
using System.Reactive;
using ReactiveUI;
using UIKit;
using System.Reactive.Linq;
using Hdp.CoreRx.ViewModels;
using Splat;
using Hdp.CoreRx.Services;

namespace Hdp.TouchRx.ViewControllers
{
    public class BaseCollectionViewController<TViewModel> : ReactiveCollectionViewController, IViewFor<TViewModel> where TViewModel : class
    {
        private TViewModel _viewModel;
        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel
        {
            get { return _viewModel; }
            set { ViewModel = (TViewModel)value; }
        }

        protected BaseCollectionViewController (UICollectionViewLayout layout) :base (layout)
        {
            this.WhenAnyValue(x => x.ViewModel)
                .OfType<IProvidesTitle>()
                .Select(x => x.WhenAnyValue(y => y.Title))
                .Switch().Subscribe(x => Title = x ?? string.Empty);

            this.WhenAnyValue(x => x.ViewModel)
                .OfType<IRoutingViewModel>()
                .Select(x => x.RequestNavigation)
                .Switch()
                .Subscribe(x => {
                    var viewModelViewService = Locator.Current.GetService<IViewModelViewService>();
                    var serviceConstructor = Locator.Current.GetService<IServiceConstructor>();
                    var viewType = viewModelViewService.GetViewFor(x.GetType());
                    var view = (IViewFor)serviceConstructor.Construct(viewType);
                    view.ViewModel = x;
                    HandleNavigation(x, view as UIViewController);
                });
        }

        protected virtual void HandleNavigation(IBaseViewModel viewModel, UIViewController view)
        {
            if (view is IModalView)
            {
                view.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s, e) => DismissViewController(true, null));
                PresentViewController(new UINavigationController(view), true, null);
                viewModel.RequestDismiss.Subscribe(_ => DismissViewController(true, null));
            }
            else
            {
                NavigationController.PushViewController(view, true);
                viewModel.RequestDismiss.Subscribe(_ => NavigationController.PopToViewController(this, true));
            }
        }
    }
}

