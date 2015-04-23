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

[assembly: ExportRenderer(typeof(SwitchMediaSample.ButtonPage), typeof(SwitchMediaSample.iOS.Controls.ButtonPageRenderer))]

namespace SwitchMediaSample.iOS.Controls
{
    public class ButtonPageRenderer : PageRenderer
    {
        CustomButton button;
        UITextView textView;

        MPMoviePlayerController moviePlayer;

        public ButtonPageRenderer()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //button = new CustomButton { Frame = new CGRect(100, 100, 300, 100) };
            //button.SetTitle("Custom button", UIControlState.Normal);
            //View.AddSubview(button);

            FileInfo fi = new FileInfo("sample.m4v");

            textView = new UITextView { Frame = new CGRect(100, 100, 300, 100) };
            textView.Text = "File exists: " + fi.Exists;
            View.AddSubview(textView);

            moviePlayer = new MPMoviePlayerController(NSUrl.FromFilename("sample.m4v"));
            moviePlayer.View.Frame = new CGRect(0, this.View.Frame.Height / 2, this.View.Frame.Width, this.View.Frame.Height / 2);
            View.AddSubview(moviePlayer.View);
            //moviePlayer.SetFullscreen(true, true);
            
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            moviePlayer.Play();
        }
    }
}