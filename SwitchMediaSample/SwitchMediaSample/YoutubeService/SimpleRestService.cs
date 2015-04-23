using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class SimpleRestService
    {
        public SimpleRestService()
        {
        }

        public void MakeRequest<T>(string requestUrl, string verb, Action<T> successAction, Action<Exception> errorAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = verb;
            request.Accept = "application/json";

            MakeRequest(
               request,
               (response) =>
               {
                   if (successAction != null)
                   {
                       T toReturn;
                       try
                       {
                           //Debug.WriteLine("Response: " + response);
                           toReturn = Deserialize<T>(response);
                       }
                       catch (Exception ex)
                       {
                           errorAction(ex);
                           return;
                       }
                       successAction(toReturn);
                   }
               },
               (error) =>
               {
                   if (errorAction != null)
                   {
                       errorAction(error);
                   }
               }
            );
        }

        private void MakeRequest(HttpWebRequest request, Action<string> successAction, Action<Exception> errorAction)
        {
            request.BeginGetResponse(token =>
            {
                try
                {
                    using (var response = request.EndGetResponse(token))
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            var reader = new StreamReader(stream);
                            string data = reader.ReadToEnd();
                            successAction(data);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Debug.WriteLine(String.Format("ERROR: '{0}' when making {1} request to {2}", ex.Message, request.Method, request.RequestUri.AbsoluteUri));
                    errorAction(ex);
                }
            }, null);
        }

        private T Deserialize<T>(string responseBody)
        {
            var toReturn = JsonConvert.DeserializeObject<T>(responseBody);
            return toReturn;
        }
    }
}
