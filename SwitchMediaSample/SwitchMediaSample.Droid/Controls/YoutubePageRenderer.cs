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

[assembly: ExportRenderer(typeof(SwitchMediaSample.YoutubePage), typeof(SwitchMediaSample.Droid.Controls.YoutubePageRenderer))]
namespace SwitchMediaSample.Droid.Controls
{
    public class YoutubePageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);            
        }
    }
}