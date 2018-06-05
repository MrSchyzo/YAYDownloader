using System;
using System.Collections.Generic;
using System.Windows.Input;
using YTDownloaderWpf.Tasks.Models;
using YTDownloaderWpf.Views.Dialogs;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class AddTaskCommand : ICommand
    {
        private ICollection<MetadataTask> metadata;

        public AddTaskCommand(ICollection<MetadataTask> metadata)
        {
            this.metadata = metadata;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TaskDialog dialog = new TaskDialog();

            if (!(dialog.ShowDialog() ?? false))
                return;

            metadata.Add(new MetadataTask()
            {
                Url = dialog.URL,
                Name = dialog.TaskName,
                Status = MetadataState.READY,
                Message = Properties.Resources.METADATA_TASK_READY
            });
        }
    }
}