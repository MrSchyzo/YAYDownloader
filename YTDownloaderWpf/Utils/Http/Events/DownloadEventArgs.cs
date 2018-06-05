using System;

namespace YTDownloaderWpf.Utils.Http.Events
{
    public class DownloadEventArgs : EventArgs, IDownloadEventArgs
    {
        public byte[] Buffer { get; set; }

        public int ReadBytes { get; set; }

        public long TotBytes { get; set; }
    }
}
