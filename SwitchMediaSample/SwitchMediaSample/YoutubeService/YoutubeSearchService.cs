using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class YoutubeSearchService : SimpleRestService
    {
        public readonly string ApiKey = "";

        public YoutubeSearchService(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public void Search<T>(string searchTerm, Action<T> successAction, Action<Exception> errorAction)
        {
            string uri = "https://www.googleapis.com/youtube/v3/search?" +
                     "part=snippet" +
                     "&q=" + Uri.EscapeDataString(searchTerm) + 
                     "&type=video" +
                     "&key=" + ApiKey;

            Debug.WriteLine("search: " + uri);

            this.MakeRequest(uri, "GET", successAction, errorAction);
        }
    }
}
