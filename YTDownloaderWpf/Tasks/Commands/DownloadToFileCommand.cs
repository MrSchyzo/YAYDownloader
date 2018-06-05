using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoLibrary;
using YTDownloaderWpf.Tasks.Models;
using YTDownloaderWpf.Utils.Extensions;
using YTDownloaderWpf.Utils.Http;
using YTDownloaderWpf.Utils.Http.Events;
using YTDownloaderWpf.Utils.Http.Handlers;
using YTDownloaderWpf.Utils.PathGetters;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class DownloadToFileCommand : ICommand
    {
        private IPathGetter pathGetter;

        public event EventHandler CanExecuteChanged;

        public DownloadToFileCommand() : this(new SaveFilePathGetter())
        {
        }

        public DownloadToFileCommand(IPathGetter pathGetter)
        {
            this.pathGetter = pathGetter;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MetadataTask task = (MetadataTask)parameter;
            if (task.Status != MetadataState.METADATA)
                return;

            pathGetter.SetExtensionFilter(task.SelectedVideo.GetSuggestedExtensions());
            pathGetter.SetSuggestedFilename(task.SuggestedFilename);
            string pathToSave = pathGetter.GetPath();
            if (pathToSave == null)
                return;

            Task.Run(async () => await DownloadToFile(task, pathToSave));
        }

        private async Task DownloadToFile(MetadataTask task, string pathToSave)
        {
            YouTubeVideo vid = task.SelectedVideo.YoutubeVideo;
            long totalDownloaded = 0;

            try
            {
                task.Status = MetadataState.RUNNING;
                task.Message = "Retrieving chosen video URI";
                using (ChunkDownloader client = new ChunkDownloader())
                {
                    client.OnProgress += (new ProgressHandler(task)).HandleProgress;

                    totalDownloaded = await client.DownloadToFile(vid.Uri, pathToSave);
                }
                task.Status = MetadataState.METADATA;
                task.Message = $"File correctly downloaded {totalDownloaded.GetBeautifiedByteSize()} to \"{pathToSave}\"";
            }
            catch (Exception ex)
            {
                task.Status = MetadataState.ERROR;
                task.Message = ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }
    }

    internal class ProgressHandler : IDownloadProgressHandler
    {
        private MetadataTask task;
        private int accumulate = 0;

        internal ProgressHandler(MetadataTask task)
        {
            this.task = task;
        }

        public void HandleProgress(object sender, IDownloadEventArgs e)
        {
            var size = (accumulate += e.ReadBytes).GetBeautifiedByteSize();
            task.Message = $"Downloading... {size}";
        }
    }
}
