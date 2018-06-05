using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using YTDownloaderWpf.Utils.Http.Events;

namespace YTDownloaderWpf.Utils.Http
{
    public class ChunkDownloader : HttpClient, IDisposableProgressFileDownloader
    {
        private int chunkSize = 128 * 1024;
        
        public event EventHandler<IDownloadEventArgs> OnProgress;
        protected void Progress(IDownloadEventArgs progressArg)
        {
            if (OnProgress == null)
                return;

            OnProgress(this, progressArg);
        }

        public async Task<long> DownloadToFile(string url, string filePath)
        {
            long total = 0;
            
            using (FileStream writingStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await ProcessRequestStream(
                    url, 
                    async (bytes, readBytes, tot) => {
                        total = tot == null ? total + readBytes : (tot ?? 0);
                        await writingStream.WriteAsync(bytes, 0, readBytes);
                        Progress(new DownloadEventArgs() { Buffer = bytes, ReadBytes = readBytes, TotBytes = tot ?? 0 });
                    });
            }

            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="streamChunkProcessing">An async void function that gets 3 values: read bytes buffer, number of read bytes, total bytes</param>
        /// <returns></returns>
        public async Task ProcessRequestStream(string url, Func<byte[], int, long?, Task> streamChunkProcessing)
        {
            byte[] streamBuffer = new byte[chunkSize];
            int readBytes = 0;
            using (Stream requestStream = await GetStreamAsync(url))
            {
                while ((readBytes = await requestStream.ReadAsync(streamBuffer, 0, chunkSize)) > 0)
                    await streamChunkProcessing(streamBuffer, readBytes, null);
            }
        }
    }
}
