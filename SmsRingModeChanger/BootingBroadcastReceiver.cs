using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.smartgnan.SmsRingModeChanger
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted })]
    public class BootingBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Intent.ActionBootCompleted))
            {
                if (!SGSmsRMCService.isRunning)
                {
                    Type t = typeof(SGSmsRMCService);
                    ComponentName n = context.StartService(new Intent(context, t));
                }
            }
        }
    }
}