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
using Android.Webkit;

namespace WebView.Plugin.Droid
{
    [Activity(Label = "FilePickerActivity")]
    public class LaunchFilePickerActivity : Activity
    {
        public static IValueCallback UploadMessage;
        private int FILECHOOSER_RESULTCODE = 1;
        private bool IsFilePicked;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.IsFilePicked = false;

            if (savedInstanceState != null)
            {
                
            }

            // Create your application here
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (!this.IsFilePicked)
            {
                Intent i = new Intent(Intent.ActionGetContent);
                i.AddCategory(Intent.CategoryOpenable);
                i.SetType("*/*");
                this.StartActivityForResult(Intent.CreateChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE);
            }
            else
            {
                this.Finish();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == FILECHOOSER_RESULTCODE)
            {
                if (LaunchFilePickerActivity.UploadMessage != null)
                {
                    Android.Net.Uri[] result = data == null || resultCode != Result.Ok ? null : new Android.Net.Uri[] { data.Data };

                    try
                    {
                        LaunchFilePickerActivity.UploadMessage.OnReceiveValue(result);
                        this.IsFilePicked = true;

                    }
                    catch (Exception e)
                    {
                    }

                    LaunchFilePickerActivity.UploadMessage = null;
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}