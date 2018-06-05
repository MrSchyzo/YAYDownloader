using System;

namespace YTDownloaderWpf.Utils.Extensions
{
    public static class ByteNumberBeautifier
    {
        public static string GetBeautifiedByteSize(this int sizeInBytes)
        {
            return ((long)sizeInBytes).GetBeautifiedByteSize();
        }

        public static string GetBeautifiedByteSize(this long sizeInBytes)
        {
            string[] orders = new string[]
            {
                "",
                "k",
                "M",
                "G",
                "T",
                "P",
                "H"
            };

            var power = Math.Floor(Math.Log(sizeInBytes, 1024));
            var number = Math.Round(sizeInBytes / Math.Pow(1024, power), 2);
            var order = orders[(int)power];

            return $"{number}{order}B";
        }
    }
}
