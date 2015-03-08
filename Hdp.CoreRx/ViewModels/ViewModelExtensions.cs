using System;
using Splat;
using Hdp.CoreRx.Services;

namespace Hdp.CoreRx.ViewModels
{
    public static class ViewModelExtensions
    {
        public static T CreateViewModel<T> (this IBaseViewModel @this)
        {
            return Locator.Current.GetService<IServiceConstructor> ().Construct<T>();
        }
    }
}

