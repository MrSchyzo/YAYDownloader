using System.Collections.ObjectModel;
using System.Windows.Input;
using YTDownloaderWpf.Tasks.Commands;
using YTDownloaderWpf.Tasks.Models;

namespace YTDownloaderWpf.ViewModels
{
    public class VideosListViewModel
    {
        private ObservableCollection<MetadataTask> tasks = new ObservableCollection<MetadataTask>();
        public ObservableCollection<MetadataTask> Tasks
        {
            get { return tasks; }
        }

        private ICommand removeTask;
        public ICommand RemoveTask
        {
            get
            {
                return removeTask ?? (removeTask = new RemoveTaskCommand(tasks));
            }
        }

        private ICommand addTask;
        public ICommand AddTask
        {
            get
            {
                return addTask ?? (addTask = new AddTaskCommand(tasks));
            }
        }
        
        public VideosListViewModel()
        {
        }
    }
}
