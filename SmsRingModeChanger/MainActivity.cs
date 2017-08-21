using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;
using System.IO;

namespace com.smartgnan.SmsRingModeChanger
{
    [Activity(Theme = "@style/Theme.Custom", Label = "Sms RingMode Changer", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            if (!SGSmsRMCService.isRunning)
            {
                System.Type t = typeof(SGSmsRMCService);
                ComponentName n = ApplicationContext.StartService(new Intent(ApplicationContext, t));
            }

            EditText phone1 = FindViewById<EditText>(Resource.Id.phone1);
            EditText phone2 = FindViewById<EditText>(Resource.Id.phone2);
            EditText phone3 = FindViewById<EditText>(Resource.Id.phone3);
            EditText phone4 = FindViewById<EditText>(Resource.Id.phone4);
            EditText phone5 = FindViewById<EditText>(Resource.Id.phone5);

            string path = ApplicationContext.FilesDir.Path;
            var file = Path.Combine(path, "NumbersList");

            if (File.Exists(file))
            {
                string[] list = File.ReadAllLines(file);

                phone1.Text = list[0];
                phone2.Text = list[1];
                phone3.Text = list[2];
                phone4.Text = list[3];
                phone5.Text = list[4];
            }

            Button save = FindViewById<Button>(Resource.Id.savebutton);
            save.Click += Save_Click;

            Button link = FindViewById<Button>(Resource.Id.linkButton);
            link.Click += Link_Click;
        }

        private void Link_Click(object sender, System.EventArgs e)
        {
            Intent browserIntent = new Intent("android.intent.action.VIEW", Uri.Parse("http://www.smartgnan.com"));
            StartActivity(browserIntent);
        }

        private void Save_Click(object sender, System.EventArgs e)
        {
            TextView fail = FindViewById<TextView>(Resource.Id.fail);
            TextView success = FindViewById<TextView>(Resource.Id.success);

            fail.Visibility = Android.Views.ViewStates.Invisible;
            success.Visibility = Android.Views.ViewStates.Invisible;
            
            EditText phone1 = FindViewById<EditText>(Resource.Id.phone1);
            EditText phone2 = FindViewById<EditText>(Resource.Id.phone2);
            EditText phone3 = FindViewById<EditText>(Resource.Id.phone3);
            EditText phone4 = FindViewById<EditText>(Resource.Id.phone4);
            EditText phone5 = FindViewById<EditText>(Resource.Id.phone5);

            string[] list = { phone1.Text, phone2.Text, phone3.Text, phone4.Text, phone5.Text };

            foreach(string s in list)
            {
                if(s.Length !=0 && s.Length < 10)
                {
                    fail.Visibility = Android.Views.ViewStates.Visible;
                    Toast.MakeText(this, "Saving failed, Mobile number should be 10 digits long.", ToastLength.Short).Show();
                    return;
                }
            }

            string path = ApplicationContext.FilesDir.Path;
            var file = Path.Combine(path, "NumbersList");
            File.WriteAllLines(file, list);
            success.Visibility = Android.Views.ViewStates.Visible;
            Toast.MakeText(this, "Saved your inputs.", ToastLength.Short).Show();
        }
    }
}

