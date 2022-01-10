using System.IO;

namespace LightImage.Interactions.Files
{

    public class SelectFolderOutput
    {
        public SelectFolderOutput(DirectoryInfo folder)
        {
            Folder = folder;
        }

        public DirectoryInfo Folder { get; }
    }
}