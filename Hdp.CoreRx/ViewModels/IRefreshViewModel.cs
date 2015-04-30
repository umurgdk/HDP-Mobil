using System;
using ReactiveUI;
using System.Reactive;

namespace Hdp.CoreRx.ViewModels
{
    public interface IRefreshViewModel
    {
        IReactiveCommand<object> RefreshContent {get;}
    }
}

