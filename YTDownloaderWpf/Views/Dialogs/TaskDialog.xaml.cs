using System;
using System.Windows;
using YTDownloaderWpf.Utils.Extensions;

namespace YTDownloaderWpf.Views.Dialogs
{
    public partial class TaskDialog : Window
    {
        public TaskDialog()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUrl.Text) || txtUrl.Text.IsValidHttpUri())
            {
                DialogResult = true;
                return;
            }

            MessageBox.Show(Properties.Resources.DIALOG_INVALID_URL, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            DialogResult = false;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtUrl.Focus();
        }

        public string URL
        {
            get { return txtUrl.Text; }
        }

        public string TaskName
        {
            get { return txtName.Text; }
        }
    }
}