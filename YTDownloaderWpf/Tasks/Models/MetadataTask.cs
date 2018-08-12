using YTDownloaderWpf.ComponentModel;
using YTDownloaderWpf.Utils.Extensions;
using VideoLibrary;
using System.Collections.Generic;

namespace YTDownloaderWpf.Tasks.Models
{
    public class MetadataTask : TrivialObservable
    {
        #region Task properties
        public string SuggestedFilename { get { return Name.GetValidFilePath(); } }
        #endregion


        #region Task read-write properties
        #region Task url
        private string url = "";
        public string Url {
            get { return url; }
            set
            {
                if (!url.Equals(value))
                {
                    url = value;
                    OnPropertyChanged("Url");
                }
            }
        }
        #endregion

        #region Task url
        private string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                if (!name.Equals(value))
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        #endregion

        #region Task message
        private string message = "";
        public string Message
        {
            get { return message; }
            set
            {
                if (!message.Equals(value))
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        #endregion


        #region Task status
        private MetadataState status = MetadataState.READY;
        public MetadataState Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        #endregion

        #region Task payload
        ICollection<YouTubeVideoWrapper> videos = new List<YouTubeVideoWrapper>();

        public void AddVideoToCollection(YouTubeVideo vid)
        {
            videos.Add(new YouTubeVideoWrapper(vid));
            OnPropertyChanged("Videos");
        }

        public void AddVideoToCollection(IEnumerable<YouTubeVideo> vids)
        {
            foreach (var vid in vids)
                AddVideoToCollection(vid);
        }

        public void ClearVideoCollection()
        {
            videos.Clear();
            OnPropertyChanged("Videos");
        }

        public List<YouTubeVideoWrapper> Videos { get => new List<YouTubeVideoWrapper>(videos); }
        #endregion

        private YouTubeVideoWrapper selectedVideo;
        public YouTubeVideoWrapper SelectedVideo { get { return selectedVideo; }
            set
            {
                if (selectedVideo == null || !selectedVideo.Equals(value))
                {
                    selectedVideo = value;
                    OnPropertyChanged("SelectedVideo");
                }
            }
        }
        #endregion

        public MetadataTask() { }
    }

    #region Task status enumeration
    public enum MetadataState
    {
        READY,
        RUNNING,
        METADATA,
        ERROR
    }
    #endregion
}
