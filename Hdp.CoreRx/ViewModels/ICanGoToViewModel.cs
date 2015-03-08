using System;
using ReactiveUI;

namespace Hdp.CoreRx.ViewModels
{
    public interface ICanGoToViewModel
    {
        IReactiveCommand<object> GoToCommand { get; }
    }
}

