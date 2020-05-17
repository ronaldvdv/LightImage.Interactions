using System;
using System.IO;
using System.Threading.Tasks;
using LightImage.Interactions.Files;

namespace LightImage.Interactions
{
    /// <summary>
    /// Extension methods for interactions related to selecting files.
    /// </summary>
    public static class FileInteractionExtensions
    {
        public static async Task<FileInfo[]> OpenFile(this IInteractionService service, OpenFileInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = await service.Show<OpenFileInput, OpenFileOutput>(input);
            return result?.Files;
        }

        public static Task<FileInfo[]> OpenFile(
            this IInteractionService service,
            string title = "Open file",
            string defaultExtension = "",
            string filter = "All files (*.*)|*.*",
            string path = "",
            bool multiSelect = false
        )
        {
            return OpenFile(service, new OpenFileInput { DefaultExtension = defaultExtension, Filter = filter, MultiSelect = multiSelect, Path = path, Title = title });
        }

        public static async Task<FileInfo> SaveFile(this IInteractionService service, SaveFileInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = await service.Show<SaveFileInput, SaveFileOutput>(input);
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