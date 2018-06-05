using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoLibrary;
using YTDownloaderWpf.Tasks.Models;
using YTDownloaderWpf.Utils.Http;
using YTDownloaderWpf.Utils.PathGetters;
using NReco.VideoConverter;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class DownloadMP3Command : ICommand
    {
        private IPathGetter pathGetter;

        public event EventHandler CanExecuteChanged;

        public DownloadMP3Command() : this(new SaveFilePathGetter())
        {
        }

        public DownloadMP3Command(IPathGetter pathGetter)
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

            pathGetter.SetExtensionFilter(new string[] { });
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
            string tempFile = $"{pathToSave}.tmp";
            string mp3File = $"{pathToSave}.mp3";

            try
            {
                task.Status = MetadataState.RUNNING;
                task.Message = "Retrieving chosen video URI";
                using (ChunkDownloader client = new ChunkDownloader())
                {
                    client.OnProgress += (new ProgressHandler(task)).HandleProgress;

                    totalDownloaded = await client.DownloadToFile(vid.Uri, tempFile);
                }
                task.Message = $"Now extracting MP3 from \"{tempFile}\"";

                FFMpegConverter converter = new FFMpegConverter();
                converter.ConvertProgress += (new ConvertProgressHandler(task)).HandleProgress;
                converter.ConvertMedia(tempFile, mp3File, "mp3");
                System.IO.File.Delete(tempFile);

                task.Status = MetadataState.METADATA;
                task.Message = $"MP3 correctly downloaded to \"{mp3File}\"";
            }
            catch (Exception ex)
            {
                task.Status = MetadataState.ERROR;
                task.Message = ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }
    }

    internal class ConvertProgressHandler
    {
        private MetadataTask task;

        internal ConvertProgressHandler(MetadataTask task)
        {
            this.task = task;
        }

        public void HandleProgress(object sender, ConvertProgressEventArgs e)
        {
            task.Message = $"Converting to mp3: {e.Processed} out of {e.TotalDuration}";
        }
    }
}
