using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoLibrary;
using YTDownloaderWpf.Tasks.Models;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class DownloadMetadataCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MetadataTask task = (MetadataTask)parameter;

            Task.Run(async () => await FetchMetadata(task));
        }

        private async Task FetchMetadata(MetadataTask task)
        {
            if (task.Status != MetadataState.READY)
                return;

            try
            {
                task.ClearVideoCollection();

                task.Status = MetadataState.RUNNING;
                task.Message = "Fetching metadata";

                var videos = await (new YouTube()).GetAllVideosAsync(task.Url);
                var downloadableVideos = videos.Where(x => x.Resolution > 0 || x.AudioFormat != AudioFormat.Unknown);
                task.AddVideoToCollection(downloadableVideos);

                task.Status = MetadataState.METADATA;
                task.Message = $"Got {task.Videos.Count} (out of {videos.Count()}) downloadable URLs ";
            }
            catch (Exception ex)
            {
                task.Status = MetadataState.ERROR;
                task.Message = ex.Message;
            }
        }
    }
}
