using System;
using System.Collections.Generic;
using System.Windows.Input;
using YTDownloaderWpf.Tasks.Models;

namespace YTDownloaderWpf.Tasks.Commands
{
    public class RemoveTaskCommand : ICommand
    {
        private ICollection<MetadataTask> tasks;
        public event EventHandler CanExecuteChanged;

        public RemoveTaskCommand(ICollection<MetadataTask> tasks)
        {
            this.tasks = tasks;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MetadataTask metadataTask = parameter as MetadataTask;
            if (metadataTask == null)
                return;

            tasks.Remove(metadataTask);
        }
    }
}
