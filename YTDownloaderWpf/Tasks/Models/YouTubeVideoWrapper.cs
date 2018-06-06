using System;
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

            var audio = GetAudioDescription(vid);
            var video = GetVideoDescription(vid);

            return string.Join(" ; ", new string[] { video, audio }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private static string GetVideoDescription(YouTubeVideo vid)
        {
            if (vid.Resolution <= 0)
                return String.Format(Properties.Resources.YTVIDEO_WRAPPER_NOVIDEO);

            return String.Format(Properties.Resources.YTVIDEO_WRAPPER_VIDEOFORMAT, vid.Resolution, vid.Format, vid.FileExtension);
        }

        private static string GetAudioDescription(YouTubeVideo vid)
        {
            if (vid.AudioFormat == AudioFormat.Unknown)
                return String.Format(Properties.Resources.YTVIDEO_WRAPPER_NOAUDIO);

            return String.Format(Properties.Resources.YTVIDEO_WRAPPER_AUDIOFORMAT_BITRATE, vid.AudioFormat, vid.AudioBitrate);
        }
    }
}
