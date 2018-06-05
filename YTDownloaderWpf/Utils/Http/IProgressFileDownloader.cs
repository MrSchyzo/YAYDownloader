using System;
using System.Threading.Tasks;
using YTDownloaderWpf.Utils.Http.Events;

namespace YTDownloaderWpf.Utils.Http
{
    public interface IProgressFileDownloader
    {
        event EventHandler<IDownloadEventArgs> OnProgress;

        Task<long> DownloadToFile(string url, string filePath);

        Task ProcessRequestStream(string url, Func<byte[], int, long?, Task> streamChunkProcessing);
    }
}
