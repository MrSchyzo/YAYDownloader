using System.Collections.Generic;

namespace YTDownloaderWpf.Utils.PathGetters
{
    public interface IPathGetter
    {
        string GetPath();

        void SetExtensionFilter(IEnumerable<string> extensions);

        void SetSuggestedFilename(string fileName);
    }
}
