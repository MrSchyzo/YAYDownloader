using System.Collections.ObjectModel;
using System.Windows.Input;
using YTDownloaderWpf.Tasks.Commands;
using YTDownloaderWpf.Tasks.Models;

namespace YTDownloaderWpf.ViewModels
{
    public class VideosListViewModel
    {
        public ObservableCollection<MetadataTask> Tasks { get; } = new ObservableCollection<MetadataTask>();

        private ICommand removeTask;
        public ICommand RemoveTask
        {
            get
            {
                return removeTask ?? (removeTask = new RemoveTaskCommand(Tasks));
            }
        }

        private ICommand addTask;
        public ICommand AddTask
        {
            get
            {
                return addTask ?? (addTask = new AddTaskCommand(Tasks));
            }
        }

        private ICommand addFromClipboard;
        public ICommand AddFromClipboard
        {
            get
            {
                return addFromClipboard ?? (addFromClipboard = new AddFromClipboardCommand(Tasks));
            }
        }

        public VideosListViewModel()
        {
        }
    }
}
