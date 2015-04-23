Sample app where user can search for and view YouTube videos

Uses Xamarin Forms

Uses a WebView to host the youtube iframe client

On Android uses a custom WebViewRenderer to get around a problem with rendering video, 
we need to use WebView.SetWebChromeClient(new WebChromeClient()) otherwise we get a grey screen
instead of a video.

Uses the YouTube search api to find videos.

License:
Code may not be used without express permission from the author.