using System;
using Android.App;
using Splat;

using Hdp.CoreRx.Services;
using Hdp.CoreRx;

using Hdp.Droid.Services;
using Android.Runtime;

namespace Hdp.Droid
{
    [Application]
    public class HDPApplication : Application
    {
        HDPApp app;

        public HDPApplication (IntPtr handle, JniHandleOwnership transfer)
            :base(handle, transfer)
        {
        }

        public override void OnCreate ()
        {
            base.OnCreate ();

            Locator.CurrentMutable.InitializeServices ();

            app = new HDPApp(DeviceType.ios2x);
            app.Bootstrap ();

            var viewModelViews = Locator.Current.GetService<IViewModelViewService> ();
            viewModelViews.RegisterViewModels (typeof(MainActivity).Assembly);
        }
    }
}

