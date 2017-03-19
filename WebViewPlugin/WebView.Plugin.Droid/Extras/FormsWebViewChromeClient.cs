using Android.App;
using Android.Content;
using Android.Webkit;
using WebView.Plugin.Droid;

namespace Xam.Plugin.Droid.Extras
{
    public class FormsWebViewChromeClient : WebChromeClient
    {

        private FormsWebViewRenderer Renderer;

        public FormsWebViewChromeClient(FormsWebViewRenderer renderer)
        {
            Renderer = renderer;
        }

        [Java.Interop.Export]
        public override bool OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            var activity = new WebView.Plugin.Droid.LaunchFilePickerActivity();
            LaunchFilePickerActivity.UploadMessage = filePathCallback;

            Intent startActivity = new Intent();
            startActivity.SetClass(Application.Context, typeof(WebView.Plugin.Droid.LaunchFilePickerActivity));
            startActivity.SetFlags(ActivityFlags.NewTask);

            Android.App.Application.Context.StartActivity(startActivity);

            return true;
        }
    }
}