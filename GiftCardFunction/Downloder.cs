using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GiftCardFunction
{
    public class Downloder
    {
        public FileStreamResult DownloadImg(string path)
        {
            var stream = File.OpenRead(path);
            return new FileStreamResult(stream, "application/octet-stream")
            { FileDownloadName = "Hero.jpg" };
        }
    }
}
