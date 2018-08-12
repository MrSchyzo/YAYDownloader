using System;

namespace YTDownloaderWpf.Utils.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidHttpUri(this string uri)
        {
            Uri uriResult;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
                return false;

            var scheme = uriResult.Scheme;
            if (scheme != Uri.UriSchemeHttp && scheme != Uri.UriSchemeHttps)
                return false;

            return true;
        }

        public static string ForceEllipsis(this string text, int lengthBeforeEllipsis)
        {
            if (text.Length <= lengthBeforeEllipsis)
                return text;

            return $"{text.Substring(0, lengthBeforeEllipsis)}...";
        }
    }
}
