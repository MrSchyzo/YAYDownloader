using System.Linq;

namespace YTDownloaderWpf.Utils.Extensions
{
    public static class PathExtensions
    {
        public static string GetValidFilePath(this string path)
        {
            char[] invalidPathChars = System.IO.Path.GetInvalidPathChars();
            char[] invalidNameChars = System.IO.Path.GetInvalidFileNameChars();
            var invalidChars = invalidPathChars.Union(invalidNameChars);

            char[] validCharArray = path.ToCharArray().Where(x => !invalidChars.Contains(x)).ToArray();

            return new string(validCharArray);
        }
    }
}
