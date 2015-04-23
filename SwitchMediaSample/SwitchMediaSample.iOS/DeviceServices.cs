using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Reflection;
using EmbeddedResources;
using SwitchMediaSample.iOS;

[assembly: Dependency(typeof(DeviceServices))]
namespace SwitchMediaSample.iOS
{
    public class DeviceServices : IDeviceServices
    {
        public string Get()
        {
            return "";
        }

        public string GetHtml()
        {
            return ResourceLoader.GetEmbeddedResourceString(Assembly.GetAssembly(typeof(ResourceLoader)), "YTPlayerView-iframe-player.html");
        }

        public string ApiKey
        {
            get { return "AIzaSyBts3O0NDrUf53A0iVzAQjPzFT76_waDDU"; }
        }
    }
}