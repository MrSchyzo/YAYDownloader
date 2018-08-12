using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using YTDownloaderWpf.Tasks.Models;
using YTDownloaderWpf.Utils.Extensions;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class AddFromClipboardCommand : ICommand
    {
        private ICollection<MetadataTask> metadata;

        public AddFromClipboardCommand(ICollection<MetadataTask> metadata)
            => this.metadata = metadata;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            string url = Clipboard.GetText();

            if (url == null || !url.IsValidHttpUri())
            {
                MessageBox.Show(
                    String.Format(Properties.Resources.CLIPBOARD_INVALID_URL, url?.ForceEllipsis(150)),
                    Properties.Resources.CLIPBOARD_INVALID_PASTE, 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Exclamation
                    );
                return;
            }

            metadata.Add(new MetadataTask()
            {
                Url = url,
                Name = "",
                Status = MetadataState.READY,
                Message = Properties.Resources.METADATA_TASK_READY
            });
        }
    }
}