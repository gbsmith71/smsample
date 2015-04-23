using EmbeddedResources;
using SwitchMediaSample.YoutubeService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace SwitchMediaSample
{
    public class YoutubeView : ContentPage
    {
        protected WebView webView;
        protected Entry searchBox;
        protected ListView searchResults;
        protected ScrollView scrollView;
        protected Label title;

        protected YoutubeSearchService youtubeService;

        #region ctor
        public YoutubeView()
        {
            IDisplay display = Resolver.Resolve<IDevice>().Display;

            webView = new WebView();
            webView.Navigating += webView_Navigating;

            var h = display.Height;
            webView.WidthRequest = display.ScreenWidthInches() * display.Xdpi;
            webView.HeightRequest = display.ScreenWidthInches() * (9f / 16f) * display.Ydpi;
            
            //var button = new Button { Text = "Run" };
            //button.Clicked += button_Clicked;

            //Button b = new Button { Text = "Test" };
            //b.WidthRequest = display.ScreenWidthInches() * display.Xdpi;
            //b.HeightRequest = display.ScreenWidthInches() * (9f / 16f) * display.Ydpi;

            title = new Label();
            title.Text = "Search for a video to play...";
            
            searchBox = new Entry { Placeholder = "Search..." };
            searchBox.TextChanged += searchBox_TextChanged;

            searchResults = new ListView();
            searchResults.ItemTemplate = new DataTemplate(typeof(ImageCell));
            searchResults.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "ThumbnailURL");
            searchResults.ItemTemplate.SetBinding(ImageCell.TextProperty, "Title");

            StackLayout stackWeb = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical
            };

            stackWeb.WidthRequest = display.ScreenWidthInches() * display.Xdpi;
            stackWeb.HeightRequest = display.ScreenWidthInches() * (9f / 16f) * display.Ydpi;
            
            stackWeb.Children.Add(webView);
            Debug.WriteLine("Stack height: " + stackWeb.Height);

            searchResults.ItemSelected += (sender, e) =>
            {
                this.PlayVideo((ImageCellResult)e.SelectedItem);
            };

            StackLayout stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start
            };

            scrollView = new ScrollView
            {
                Content = stack
            };

            Content = scrollView;

            stack.Children.Add(stackWeb);
            stack.Children.Add(title);
            stack.Children.Add(searchBox);
            stack.Children.Add(searchResults);

            //GET https://www.googleapis.com/youtube/v3/search

            ///* initial width */ window.innerWidth, /* initial height */ window.innerHeight
            var source = new HtmlWebViewSource();
            source.Html = DependencyService.Get<IDeviceServices>().GetHtml().Replace("/* initial width */ window.innerWidth", webView.WidthRequest.ToString()).Replace("/* initial height */ window.innerHeight", webView.HeightRequest.ToString());
            //Debug.WriteLine(source.Html);
            webView.Source = source;

            youtubeService = new YoutubeSearchService(DependencyService.Get<IDeviceServices>().ApiKey);
        }
        #endregion
        
        #region searchBox_TextChanged
        int searchChangeCount = 0;

        async void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Run(async () =>
            {
                searchChangeCount++;
                await Task.Delay(500);
                searchChangeCount--;

                if (searchChangeCount == 0)
                    PerformSearch();
            });
        }
        #endregion

        #region PerformSearch
        private void PerformSearch()
        {
            if (searchBox.Text.Length == 0)
            {
                //this.searchResults.ItemsSource = null;
                return;
            }

            Debug.WriteLine("Perform search...");
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { this.title.Text = "Searching..."; });
            //this.searchBox.Unfocus();

            this.youtubeService.Search<SearchListResponse>(searchBox.Text, (result) =>
            {
                Debug.WriteLine(result.Items.Count);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    this.searchResults.ItemsSource = ImageCellResult.Get(result.Items);
                    this.searchBox.Unfocus();
                });

                //result.Items[0].Snippet.Title;
            },
            (result) =>
            {

            }
            );
        }
        #endregion

        #region button_Clicked
        //void button_Clicked(object sender, EventArgs e)
        //{
        //    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { webView.Eval("loadVideo();"); });
        //}
        #endregion

        #region PlayVideo
        private void PlayVideo(ImageCellResult imageCellResult)
        {
            this.PlayVideo(imageCellResult.VideoId);
            this.title.Text = imageCellResult.Title;
        }

        private void PlayVideo(string videoId)
        {
            webView.Eval("player.loadVideoById({'videoId':'" + videoId + "', 'playsinline': '1'});");
            webView.Focus();
        }
        #endregion

        #region webView_Navigating
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
                        //Debug.WriteLine("Start video");
                        //Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { webView.Eval("player.loadVideoById({'videoId':'ngElkyQ6Rhs', 'playsinline': '1'});"); });
                        //Xamarin.Forms.Device.BeginInvokeOnMainThread(() => { PlayVideo("ngElkyQ6Rhs"); });
                    });
                }
            }

            //e.Cancel
        }
        #endregion
    }
}
