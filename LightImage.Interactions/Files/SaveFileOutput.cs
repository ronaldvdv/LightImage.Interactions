using System.IO;

namespace LightImage.Interactions.Files
{
    public class SaveFileOutput
    {
        public SaveFileOutput(FileInfo file)
        {
            File = file;
        }

        public FileInfo File { get; }
    }
}