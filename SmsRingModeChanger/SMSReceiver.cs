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
using Android.Telephony;
using Android.Provider;
using Android.Media;

namespace com.smartgnan.SmsRingModeChanger
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SMSReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action.Equals("android.provider.Telephony.SMS_RECEIVED"))
            {
                Bundle bundle = intent.Extras;
                if(bundle != null)
                {
                    try
                    {
                        SmsMessage[] msgs = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
                        string path = context.FilesDir.Path;
                        var file = System.IO.Path.Combine(path, "NumbersList");

                        if (System.IO.File.Exists(file))
                        {
                            string[] list = System.IO.File.ReadAllLines(file);

                            bool isFound = false;

                            foreach(string s in list)
                            {
                                if((!String.IsNullOrEmpty(s)) && msgs[0].DisplayOriginatingAddress.Contains(s))
                                {
                                    isFound = true;
                                    break;
                                }
                            }

                            if (isFound)
                            {
                                AudioManager mobilemode = (AudioManager)context.GetSystemService(Context.AudioService);

                                switch (mobilemode.RingerMode)
                                {
                                    case RingerMode.Normal:
                                        mobilemode.SetStreamVolume(Stream.Ring, mobilemode.GetStreamMaxVolume(Stream.Ring), 0);
                                        break;
                                    case RingerMode.Silent:
                                    case RingerMode.Vibrate:
                                        mobilemode.SetStreamVolume(Stream.Ring, mobilemode.GetStreamMaxVolume(Stream.Ring), 0);
                                        mobilemode.RingerMode = RingerMode.Normal;
                                        Toast.MakeText(context, "ChangeRingerMode Request recieved from" + msgs[0].DisplayOriginatingAddress + " and changed ringer mode to Ringer with full volume", ToastLength.Short).Show();
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                    }
                }
            }
        }
    }
}