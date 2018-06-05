using YTDownloaderWpf.Utils.Http.Events;

namespace YTDownloaderWpf.Utils.Http.Handlers
{
    public interface IDownloadProgressHandler
    {
        void HandleProgress(object sender, IDownloadEventArgs e);
    }
}
