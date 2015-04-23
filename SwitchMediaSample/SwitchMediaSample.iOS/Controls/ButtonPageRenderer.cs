using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using System.IO;
using MediaPlayer;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(SwitchMediaSample.ButtonPage), typeof(SwitchMediaSample.iOS.Controls.ButtonPageRenderer))]

namespace SwitchMediaSample.iOS.Controls
{
    public class ButtonPageRenderer : PageRenderer
    {
        CustomButton button;
        UITextView textView;
        UIWebView webView;

        MPMoviePlayerController moviePlayer;

        public ButtonPageRenderer()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            button = new CustomButton { Frame = new CGRect(100, 50, 300, 40) };
            button.SetTitle("Start", UIControlState.Normal);
            button.SetTitleColor(UIColor.Black, UIControlState.Normal);
            button.BackgroundColor = UIColor.LightGray;

            button.TouchUpInside += button_TouchUpInside;

            View.AddSubview(button);

            //FileInfo fi = new FileInfo("sample.m4v");
            FileInfo fi = new FileInfo("YTPlayerView-iframe-player.html");

            textView = new UITextView { Frame = new CGRect(0, 100, this.View.Frame.Width, 100) };
            textView.Text = "File exists: " + fi.Exists;
            View.AddSubview(textView);

            //moviePlayer = new MPMoviePlayerController(NSUrl.FromFilename("sample.m4v"));
            //string url = Uri.UnescapeDataString("http%3A%2F%2Fr3---sn-ntq7yn7s.googlevideo.com%2Fvideoplayback%3Fms%3Dau%26pl%3D22%26itag%3D22%26mt%3D1429755875%26upn%3DyT7wwaWse4g%26expire%3D1429777521%26mm%3D31%26mv%3Dm%26nh%3DIgpwcjAxLnN5ZDEwKgkxMjcuMC4wLjE%26mime%3Dvideo%252Fmp4%26signature%3D210CAA965FB835A275A3E835ADF56A2485170B45.B128D30D43D9008BCEA7CAD7F2CAAF32B9394736%26source%3Dyoutube%26fexp%3D900720%252C907263%252C934954%252C938028%252C9406196%252C9406849%252C9407115%252C9407433%252C9408347%252C9408704%252C9408968%252C9409652%252C947233%252C948124%252C948605%252C948703%252C951703%252C952612%252C952637%252C957201%26dur%3D119.513%26key%3Dyt5%26initcwndbps%3D562500%26ipbits%3D0%26sver%3D3%26ratebypass%3Dyes%26id%3Do-AAwENcn-cXPdG1qscUIQPumU57FfLpKWSQe_YTeDnpBP%26sparams%3Ddur%252Cid%252Cinitcwndbps%252Cip%252Cipbits%252Citag%252Cmime%252Cmm%252Cms%252Cmv%252Cnh%252Cpl%252Cratebypass%252Csource%252Cupn%252Cexpire%26ip%3D121.44.136.196\u0026quality=hd720\u0026fallback_host=tc.v9.cache1.googlevideo.com\u0026itag=22\u0026type=video%2Fmp4%3B+codecs%3D%22avc1.64001F%2C+mp4a.40.2%22");
            //textView.Text = url;

            //HttpRequestHeader

            webView = new UIWebView(new CGRect(0, this.View.Frame.Height / 2, this.View.Frame.Width, this.View.Frame.Height / 2));
            View.AddSubview(webView);

            Debug.WriteLine("webview frame: " + this.webView.Frame);

            webView.ShouldStartLoad = webViewHandler;

            webView.MediaPlaybackRequiresUserAction = false;
            webView.LoadRequest( new NSUrlRequest(NSUrl.FromFilename("YTPlayerView-iframe-player.html")));
            //webView.LoadRequest(new NSUrlRequest(new NSUrl("http://www.google.com.au")));
            //webView.EvaluateJavascript("loadWithVideoId(")

            //Task.Delay(300);
            //webView.EvaluateJavascript("alert('hello');");


            //moviePlayer = new MPMoviePlayerController(NSUrl.FromString(url));
            //moviePlayer.View.Frame = new CGRect(0, this.View.Frame.Height / 2, this.View.Frame.Width, this.View.Frame.Height / 2);
            //View.AddSubview(moviePlayer.View);
            ////moviePlayer.SetFullscreen(true, true);

            UIDevice.Notifications.ObserveOrientationDidChange((s, e) =>
            {
                Debug.WriteLine(s + ", " + e);
            });
        }

        void button_TouchUpInside(object sender, EventArgs e)
        {
            webView.EvaluateJavascript("player.loadVideoById({'videoId':'ngElkyQ6Rhs', 'playsinline': '1'});");
        }

        protected bool webViewHandler(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navType)
        {
            //Debug.WriteLine(request + ", " + navType);

            if (request.Url.AbsoluteString.StartsWith("ytplayer://"))
            {
                Debug.WriteLine(request.Url.AbsoluteString);
            }

            if (request.Url.AbsoluteString == "ytplayer://onYouTubeIframeAPIReady")
            {
                webView.EvaluateJavascript("player.setSize(" + this.webView.Frame.Width + ", " + this.webView.Frame.Height + ");");
            }

            return true;
        } 

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            //moviePlayer.Play();
        }
    }
}