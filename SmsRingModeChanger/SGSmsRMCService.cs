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
    [Service(Enabled =true)]
    public class SGSmsRMCService : Service
    {
        public static bool isRunning = false;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            isRunning = true;
            this.ApplicationContext.RegisterReceiver(new SMSReceiver(), new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
            return StartCommandResult.StickyCompatibility;
        }
    }
}