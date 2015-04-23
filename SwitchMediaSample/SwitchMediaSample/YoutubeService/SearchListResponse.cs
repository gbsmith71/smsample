using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class SearchListResponse
    {
        public string Kind { get; set; }
        public string etag { get; set; }
        public string NextPageToken { get; set; }
        public PageInfo PageInfo { get; set; }

        public List<SearchResult> Items { get; set; }
    }
}
