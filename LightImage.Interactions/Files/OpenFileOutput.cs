using System.IO;

namespace LightImage.Interactions.Files
{
    public class OpenFileOutput
    {
        public OpenFileOutput(FileInfo[] files)
        {
            Files = files;
        }

        public FileInfo[] Files { get; }
    }
}