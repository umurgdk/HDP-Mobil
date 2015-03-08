using System;
using ReactiveUI;

namespace Hdp.CoreRx.ViewModels
{
    public interface IBaseViewModel : ISupportsActivation, IProvidesTitle, IRoutingViewModel
    {
    }
}

