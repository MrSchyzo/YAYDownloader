namespace YTDownloaderWpf.Utils.Http.Events
{
    public interface IDownloadEventArgs
    {
        byte[] Buffer { get; set; }

        int ReadBytes { get; set; }

        long TotBytes { get; set; }
    }
}
