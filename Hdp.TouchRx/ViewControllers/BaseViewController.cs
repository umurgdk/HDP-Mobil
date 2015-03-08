using System;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Subjects;
using Hdp.CoreRx.ViewModels;
using Splat;
using Hdp.CoreRx.Services;
using Foundation;
using UIKit;

namespace Hdp.TouchRx.ViewControllers
{
    public abstract class BaseViewController<TViewModel> : BaseViewController, IViewFor<TViewModel> where TViewModel : class
    {
        private TViewModel viewModel;

        public TViewModel ViewModel {
            get { return viewModel; }
            set { this.RaiseAndSetIfChanged (ref this.viewModel, value); }
        }

        object IViewFor.ViewModel
        {
            get { return viewModel; }
            set { ViewModel = (TViewModel)value; }
        }

        public BaseViewController ()
        {
            SetupRx ();
        }

        public void SetupRx ()
        {
            this.WhenAnyValue (x => x.ViewModel)
                .OfType<IProvidesTitle> ()
                .Subscribe (x => {
                    this.Title = x.Title ?? string.Empty;
                });

            this.WhenAnyValue (x => x.ViewModel)
                .OfType<IRoutingViewModel> ()
                .Select (x => x.RequestNavigation)
                .Switch()
                .Subscribe (x => {
                    var viewModelViewService = Locator.Current.GetService<IViewModelViewService>();
                    var serviceConstructor = Locator.Current.GetService<IServiceConstructor>();
                    var viewType = viewModelViewService.GetViewFor(x.GetType());
                    var view = (IViewFor)serviceConstructor.Construct(viewType);

                    view.ViewModel = x;
                    HandleNavigation(x, view as UIViewController);
                });
        }

        protected virtual void HandleNavigation (IBaseViewModel viewModel, UIViewController view)
        {
            if (view is IModalView)
            {
                // Make it cross image button
                view.NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Cancel, (s, e) => {
                    DismissViewController(true, null);
                });

                PresentViewController (new UINavigationController (view), true, null);
                viewModel.RequestDismiss.Subscribe (_ => DismissViewController (true, null));
            }
            else
            {
                NavigationController.PushViewController (view, true);
                viewModel.RequestDismiss.Subscribe (_ => this.NavigationController.PopToViewController (this, true));
            }
        }
    }

    public abstract class BaseViewController : ReactiveViewController
    {
        private readonly ISubject<bool> _appearingSubject = new Subject<bool>();
        private readonly ISubject<bool> _appearedSubject = new Subject<bool>();
        private readonly ISubject<bool> _disappearingSubject = new Subject<bool>();
        private readonly ISubject<bool> _disappearedSubject = new Subject<bool>();

        public IObservable<bool> Appearing
        {
            get { return _appearingSubject; }
        }

        public IObservable<bool> Appeared
        {
            get { return _appearedSubject; }
        }

        public IObservable<bool> Disappearing
        {
            get { return _disappearingSubject; }
        }

        public IObservable<bool> Disappeared
        {
            get { return _disappearedSubject; }
        }

        protected BaseViewController()
        {
//            NavigationItem.BackBarButtonItem = new UIBarButtonItem { Title = string.Empty };
        }

        protected BaseViewController(string nib, NSBundle bundle)
            : base(nib, bundle)
        {
//            NavigationItem.BackBarButtonItem = new UIBarButtonItem { Title = string.Empty };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _appearingSubject.OnNext(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _appearedSubject.OnNext(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _disappearingSubject.OnNext(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            _disappearedSubject.OnNext(animated);
        }
    }
}

