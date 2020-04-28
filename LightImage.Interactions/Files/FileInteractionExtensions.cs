using LightImage.Interactions.Files;
using System.IO;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class FileInteractionExtensions
    {
        public static async Task<FileInfo[]> OpenFile(this IInteractionService service, OpenFileInput input = null)
        {
            var result = await service.Show<OpenFileInput, OpenFileOutput>(input ?? new OpenFileInput());
            return result?.Files;
        }

        public static Task<FileInfo[]> OpenFile(
            this IInteractionService service,
            string title = "Open file",
            string defaultExtension = "",
            string filter = "All files (*.*)|*.*",
            string path = ""
        )
        {
            return OpenFile(service, new OpenFileInput { DefaultExtension = defaultExtension, Filter = filter, Path = path, Title = title });
        }

        public static async Task<FileInfo> SaveFile(this IInteractionService service, SaveFileInput input = null)
        {
            var result = await service.Show<SaveFileInput, SaveFileOutput>(input ?? new SaveFileInput());
            return result?.File;
        }

        public static Task<FileInfo> SaveFile(
            this IInteractionService service,
            string title = "Save file",
            string defaultExtension = "",
            string filter = "All files (*.*)|*.*",
            string path = ""
        )
        {
            return SaveFile(service, new SaveFileInput { DefaultExtension = defaultExtension, Filter = filter, Path = path, Title = title });
        }
    }
}