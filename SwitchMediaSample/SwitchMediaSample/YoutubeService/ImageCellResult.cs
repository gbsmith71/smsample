using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchMediaSample.YoutubeService
{
    public class ImageCellResult
    {
        public string ThumbnailURL { get; set; }
        public string Title { get; set; }
        public string VideoId { get; set; }

        public static List<ImageCellResult> Get(List<SearchResult> snippentResults)
        {
            List<ImageCellResult> results = new List<ImageCellResult>();

            foreach (SearchResult result in snippentResults)
            {
                results.Add(new ImageCellResult
                {
                    VideoId = result.ID.VideoID,
                    ThumbnailURL = result.Snippet.Thumbnails["default"].URL,
                    Title = result.Snippet.Title,
                });
            }

            return results;
        }
    }
}
