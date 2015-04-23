using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class Snippet
    {
        public DateTime PublishedAt {get;set;}
        public string ChannelId {get;set;}
        public string Title {get;set;}
        public string Description { get; set; }
        public Dictionary<string, Thumbnail> Thumbnails { get; set; }
    }
}
