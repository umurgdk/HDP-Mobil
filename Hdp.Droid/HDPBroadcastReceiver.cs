using System;
using Android.Content;
using Android.App;

namespace Hdp.Droid
{
    [BroadcastReceiver(Permission= "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] {"org.hdp.hdp" })]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] {"org.hdp.hdp" })]
    [IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "org.hdp.hdp"})]
    public class HDPBroadcastReceiver : BroadcastReceiver
    {
        const string TAG = "PushHandlerBroadcastReceiver";

        public override void OnReceive (Context context, Intent intent)
        {
            HDPIntentService.RunIntentInService (context, intent);
            SetResult (Result.Ok, null, null);
        }
    }
}

