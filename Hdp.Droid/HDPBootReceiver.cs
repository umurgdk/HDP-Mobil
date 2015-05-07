using System;
using Android.Content;
using Android.App;

namespace Hdp.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted })]
    public class HDPBootReceiver : BroadcastReceiver
    {
        public override void OnReceive (Context context, Intent intent)
        {
            HDPIntentService.RunIntentInService (context, intent);
            SetResult (Result.Ok, null, null);
        }
    }
}

