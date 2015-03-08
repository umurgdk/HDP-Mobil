using System;
using ReactiveUI;
using System.Reactive.Subjects;
using System.Reactive;

namespace Hdp.CoreRx.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IBaseViewModel
    {
        private readonly ViewModelActivator viewModelActivator = new ViewModelActivator();
        private readonly ISubject<IBaseViewModel> requestNavigationSubject = new Subject<IBaseViewModel>();
        private readonly ISubject<Unit> requestDismissSubject = new Subject<Unit>();

        private string title;

        public string Title {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged (ref this.title, value); }
        }

        protected void NavigateTo (IBaseViewModel viewModel)
        {
            requestNavigationSubject.OnNext (viewModel);
        }

        protected void Dismiss ()
        {
            requestDismissSubject.OnNext (Unit.Default);
        }


        public IObservable<IBaseViewModel> RequestNavigation {
            get {
                return requestNavigationSubject;
            }
        }
        public IObservable<Unit> RequestDismiss {
            get {
                return requestDismissSubject;
            }
        }
        public ViewModelActivator Activator {
            get {
                return viewModelActivator;
            }
        }
    }
}

