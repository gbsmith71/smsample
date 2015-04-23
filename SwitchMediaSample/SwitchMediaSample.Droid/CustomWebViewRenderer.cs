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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using SwitchMediaSample.Droid;

[assembly: ExportRenderer(typeof(WebView), typeof(CustomWebViewRenderer))]
namespace SwitchMediaSample.Droid
{
    public class CustomWebViewRenderer : Xamarin.Forms.Platform.Android.WebViewRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            bool controlWasNull = this.Control == null;

            base.OnElementChanged(e);

            if (controlWasNull)
            {
                // MDR 23/04/2015 - Without this calling player.loadVideo() results in grey screen with film strip icon
                ((Android.Webkit.WebView)this.Control).SetWebChromeClient(new Android.Webkit.WebChromeClient());
            }
        }

    }
}