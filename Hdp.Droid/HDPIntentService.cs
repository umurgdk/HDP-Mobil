using System;
using Android.App;
using Android.OS;
using Android.Content;
using Hdp.Droid.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Android.Support.V4.App;

namespace Hdp.Droid
{
    [Service]
    public class HDPIntentService : IntentService
    {
        static PowerManager.WakeLock WakeLock;
        static object LOCK = new object();

        public static void RunIntentInService(Context context, Intent intent)
        {
            lock (LOCK)
            {
                if (WakeLock == null)
                {
                    // This is called from BroadcastReceiver, there is no init.
                    var pm = PowerManager.FromContext(context);
                    WakeLock = pm.NewWakeLock(
                        WakeLockFlags.Partial, "My WakeLock Tag");
                }
            }

            WakeLock.Acquire();
            intent.SetClass(context, typeof(HDPIntentService));
            context.StartService(intent);
        }

        protected override async void OnHandleIntent(Intent intent)
        {
            try
            {
                Context context = this.ApplicationContext;
                string action = intent.Action;

                if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
                {
                    await HandleRegistration(context, intent);
                }
                else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
                {
                    HandleMessage(context, intent);
                }
            }
            finally
            {
                lock (LOCK)
                {
                    //Sanity check for null as this is a public method
                    if (WakeLock != null)
                        WakeLock.Release();
                }
            }
        }

        async Task HandleRegistration (Context context, Intent intent)
        {
            string registrationId = intent.GetStringExtra("registration_id");

            Console.WriteLine ("Device RegisterID: " + registrationId);

            if (registrationId != null && registrationId.Length > 0)
            {
                var parseService = new ParseService ();
                await parseService.CreateInstallation (registrationId);
            }
        }

        void HandleMessage (Context context, Intent intent)
        {
            string json = intent.GetStringExtra("data");
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>> (json);

            ShowNotification ("HDP", data ["alert"], context, intent);
        }

        void ShowNotification (string title, string message, Context context, Intent intent)
        {
            var notificationManager = NotificationManager.FromContext (context);
            var contentIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof(MainActivity)), 0);

            var builder = new NotificationCompat.Builder (this)
                .SetSmallIcon (Resource.Drawable.Icon)
                .SetContentTitle (title)
                .SetStyle (new NotificationCompat.BigTextStyle ().BigText (message))
                .SetContentText (message);

            builder.SetContentIntent (contentIntent);
            notificationManager.Notify (1, builder.Build ());
        }
    }
}