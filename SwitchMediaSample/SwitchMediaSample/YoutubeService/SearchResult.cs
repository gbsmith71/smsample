using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class SearchResult
    {
        public string Kind { get; set; }
        public string etag { get; set; }
        public ID ID { get; set; }
        public Snippet Snippet { get; set; }
    }
}
