using System.Collections.Generic;
using System.Linq;
using VideoLibrary;
using YTDownloaderWpf.Utils.Extensions;

namespace YTDownloaderWpf.Tasks.Models
{
    public class YouTubeVideoWrapper : IYouTubeVideoWrapper
    {
        private YouTubeVideo ytVideo;
        public YouTubeVideo YoutubeVideo { get { return ytVideo; } set { ytVideo = value; } }

        public YouTubeVideoWrapper(YouTubeVideo vid)
        {
            ytVideo = vid;
        }
        
        public IEnumerable<string> GetSuggestedExtensions()
        {
            if (ytVideo.Resolution > 0)
                return new string[] { ytVideo.FileExtension.TrimStart(new char[] { '.' }) };

            if (ytVideo.AudioFormat.Equals(AudioFormat.Vorbis))
                return new string[] { "ogg" };

            return new string[] { ytVideo.AudioFormat.ToString().ToLowerInvariant().TrimStart(new char[] { '.' }) };
        }

        public string GetSuggestedFilename()
        {
            return ytVideo.Title.GetValidFilePath();
        }

        public override string ToString()
        {
            YouTubeVideo vid = ytVideo;

            var audio = vid.AudioFormat != AudioFormat.Unknown ? $"Audio format {vid.AudioFormat}" : "Without audio";
            var video = vid.Resolution > 0 ? $"Video: [{vid.Resolution}p]{vid.Format} - {vid.FileExtension}" : "";

            return string.Join(" ; ", new string[] { video, audio }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
