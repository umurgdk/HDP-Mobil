using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Hdp.Core.ViewModels;

namespace Hdp.Core
{
    public class App : MvxApplication
    {
        public App ()
        {
            Mvx.RegisterSingleton<IMvxAppStart> (new MvxAppStart<ArticlesViewModel> ());
        }
    }
}

