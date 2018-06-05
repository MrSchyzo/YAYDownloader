using System.Collections.Generic;
using VideoLibrary;

namespace YTDownloaderWpf.Tasks.Models
{
    public interface IYouTubeVideoWrapper
    {
        YouTubeVideo YoutubeVideo { get; set; }

        IEnumerable<string> GetSuggestedExtensions();

        string GetSuggestedFilename();
    }
}
