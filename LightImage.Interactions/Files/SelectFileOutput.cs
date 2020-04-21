using System.IO;

namespace LightImage.Interactions.Files
{
    public class SelectFileOutput
    {
        public SelectFileOutput(FileInfo[] files)
        {
            Files = files;
        }

        public FileInfo[] Files { get; }
    }
}