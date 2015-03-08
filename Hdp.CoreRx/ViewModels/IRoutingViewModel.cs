using System;
using System.Reactive;

namespace Hdp.CoreRx.ViewModels
{
    public interface IRoutingViewModel
    {
        IObservable<IBaseViewModel> RequestNavigation { get; }
        IObservable<Unit> RequestDismiss { get; }
    }
}

