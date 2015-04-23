using EmbeddedResources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwitchMediaSample
{
    public class YoutubeView : ContentPage
    {
        WebView webView;

        public YoutubeView()
        {
            webView = new WebView();
            webView.Navigating += webView_Navigating;
            webView.Navigated += webView_Navigated;
            webView.WidthRequest = 200;
            webView.HeightRequest = 200;

            //var source = new UrlWebViewSource();
            //source.Url = DependencyService.Get<IBaseUrl>().Get() + "YTPlayerView-iframe-player.html";
            //webView.Source = source;

            //webView.get
            var source = new HtmlWebViewSource();
            source.Html = DependencyService.Get<IDeviceServices>().GetHtml();
            //DependencyService.Get<IBaseUrl>().AttachChromeView(webView);
            webView.Source = source;
            
            var button = new Button { Text = "Run" };
            button.Clicked += button_Clicked;

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    webView, button
                }

            };

            //Content = webView;
        }

        void button_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { webView.Eval("loadVideo();"); });
        }

        async void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var source = e.Source as UrlWebViewSource;

            if (source == null) return;

            if (source.Url.Contains("ytplayer://"))
            {
                Debug.WriteLine("Player event: " + source.Url);

                e.Cancel = true;

                string command = source.Url.Replace("ytplayer://", "").Split('?')[0];

                if (command.Contains("onYouTubeIframeAPIReady"))
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        Debug.WriteLine("Start video");
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { webView.Eval("player.loadVideoById({'videoId':'ngElkyQ6Rhs', 'playsinline': '1'});"); });

                    });
                }
            }

            //e.Cancel
        }

        void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
        }
    }
}
