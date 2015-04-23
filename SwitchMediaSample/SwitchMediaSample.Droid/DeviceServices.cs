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
using Xamarin.Forms;
using SwitchMediaSample.Droid;
using System.Reflection;
using EmbeddedResources;

[assembly: Dependency(typeof(DeviceServices))]
namespace SwitchMediaSample.Droid
{
    public class DeviceServices : IDeviceServices 
    {
        public string Get()
        {
            return "file:///android_asset/";
        }

        public string GetHtml()
        {
            return ResourceLoader.GetEmbeddedResourceString(Assembly.GetAssembly(typeof(ResourceLoader)), "YTPlayerView-iframe-player.html");
        }
    }
}