using System;
using Android.App;
using Splat;
using System.Threading.Tasks;

using Hdp.CoreRx.Services;
using Hdp.CoreRx;

using Hdp.Droid.Services;
using Android.Runtime;
using Android.Content;
using Android.Locations;
using Java.Util;
using Akavache;

namespace Hdp.Droid
{
    [Application]
    public class HDPApplication : Application, ILocationListener
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

            app = new HDPApp(DeviceType.ios2x, BlobCache.LocalMachine);
            app.Bootstrap ();

            var viewModelViews = Locator.Current.GetService<IViewModelViewService> ();
            viewModelViews.RegisterViewModels (typeof(MainActivity).Assembly);

            GetUserCity ();
            RegisterDevice ();
        }

        public void RegisterDevice ()
        {
            string senders = "1026338473197";
            Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
            intent.SetPackage("com.google.android.gsf");
            intent.PutExtra("app", PendingIntent.GetBroadcast(Context, 0, new Intent(), 0));
            intent.PutExtra("sender", senders);
            Context.StartService(intent);
        }

        public void GetUserCity ()
        {
            var gpsProvider = LocationManager.GpsProvider;
            var networkProvider = LocationManager.NetworkProvider;

            var locationManager = LocationManager.FromContext (this);

            if (locationManager.IsProviderEnabled(networkProvider))
            {
                locationManager.RequestSingleUpdate (networkProvider, this, MainLooper);
            }

            else if (locationManager.IsProviderEnabled(gpsProvider))
            {
                locationManager.RequestSingleUpdate (gpsProvider, this, MainLooper);
            }
        }

        public void OnLocationChanged (Location location)
        {
            var geocoder = new Geocoder (this, Java.Util.Locale.Default);
            var addresses = geocoder.GetFromLocation (location.Latitude, location.Longitude, 1);

            if (addresses.Count > 0)
            {
                app.UserCity.OnNext(addresses [0].Locality);
            }
        }

        public void OnProviderDisabled (string provider) {  }
        public void OnProviderEnabled (string provider) {  }
        public void OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras) {  }
    }
}

