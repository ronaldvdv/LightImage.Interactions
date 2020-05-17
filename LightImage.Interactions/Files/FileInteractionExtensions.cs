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
        /// <summary>
        /// Ask for opening an existing file, using a <see cref="OpenFileInput"/> view model to specify input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="input">The input to the interaction.</param>
        /// <returns>The chosen file(s) or NULL if canceled.</returns>
        public static async Task<FileInfo[]> OpenFile(this IInteractionService service, OpenFileInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = await service.Show<OpenFileInput, OpenFileOutput>(input);
            return result?.Files;
        }

        /// <summary>
        /// Ask for opening an existing file.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">The title for the interaction.</param>
        /// <param name="defaultExtension">The default extension to use if none is entered.</param>
        /// <param name="filter">File type filter; see <see cref="FileInput{TOutput}.Filter"/> for details.</param>
        /// <param name="path">Initial path to be shown.</param>
        /// <param name="multiSelect">Value indicating whether multiple files can be selected.</param>
        /// <returns>The selected file(s), or NULL if canceled.</returns>
        public static Task<FileInfo[]> OpenFile(
            this IInteractionService service,
            string title = "Open file",
            string defaultExtension = "",
            string filter = "All files (*.*)|*.*",
            string path = "",
            bool multiSelect = false)
        {
            return OpenFile(service, new OpenFileInput { DefaultExtension = defaultExtension, Filter = filter, MultiSelect = multiSelect, Path = path, Title = title });
        }

        /// <summary>
        /// Ask a filename for saving, using a <see cref="SaveFileInput"/> view model to specify input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="input">The input to the interaction.</param>
        /// <returns>The chosen file(s) or NULL if canceled.</returns>
        public static async Task<FileInfo> SaveFile(this IInteractionService service, SaveFileInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = await service.Show<SaveFileInput, SaveFileOutput>(input);
            return result?.File;
        }

        /// <summary>
        /// Ask for saving a file.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">The title for the interaction.</param>
        /// <param name="defaultExtension">The default extension to use if none is entered.</param>
        /// <param name="filter">File type filter; see <see cref="FileInput{TOutput}.Filter"/> for details.</param>
        /// <param name="path">Initial path to be shown.</param>
        /// <returns>The chosen file, or NULL if canceled.</returns>
        public static Task<FileInfo> SaveFile(
            this IInteractionService service,
            string title = "Save file",
            string defaultExtension = "",
            string filter = "All files (*.*)|*.*",
            string path = "")
        {
            return SaveFile(service, new SaveFileInput { DefaultExtension = defaultExtension, Filter = filter, Path = path, Title = title });
        }
    }
}