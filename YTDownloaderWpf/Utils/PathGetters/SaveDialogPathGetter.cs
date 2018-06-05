using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YTDownloaderWpf.Utils.PathGetters
{
    public class SaveFilePathGetter : IPathGetter
    {
        private SaveFileDialog saveDialog;

        public SaveFilePathGetter() : this(new SaveFileDialog()) { }

        public SaveFilePathGetter(SaveFileDialog dialog)
        {
            saveDialog = dialog;
        }

        public string GetPath()
        {
            if ((saveDialog.ShowDialog() ?? false) == true)
                return saveDialog.FileName;

            return null;
        }

        public void SetExtensionFilter(IEnumerable<string> extensions)
        {
            saveDialog.Filter = String.Join("|", extensions.Select(x => $"File .{x}|*.{x}").ToArray());
        }

        public void SetSuggestedFilename(string fileName)
        {
            saveDialog.FileName = fileName;
        }
    }
}
