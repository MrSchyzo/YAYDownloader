using System;
using System.Net;
using System.Windows;

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
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(txtUrl.Text);
                request.Timeout = 15000;
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    this.DialogResult = response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex) when (ex is WebException || ex is UriFormatException)
            {
                this.DialogResult = false;
            }

            if (!(this.DialogResult ?? false))
            {
                MessageBox.Show(Properties.Resources.DIALOG_INVALID_URL, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
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