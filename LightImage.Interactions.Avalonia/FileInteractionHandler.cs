using Avalonia.Controls;
using LightImage.Interactions.Files;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    /// <summary>
    /// WPF implementation of interaction handlers for files.
    /// </summary>
    public class FileInteractionHandler :
        IInteractionHandler<OpenFileInput, OpenFileOutput>,
        IInteractionHandler<SaveFileInput, SaveFileOutput>,
        IInteractionHandler<SelectFolderInput, SelectFolderOutput>
    {
        /// <inheritdoc/>
        public async Task<OpenFileOutput> Handle(OpenFileInput request, CancellationToken cancellationToken)
        {
            var dialog = new OpenFileDialog { AllowMultiple = request.MultiSelect };
            Initialize(dialog, request);
            var paths = await dialog.ShowAsync(AvaloniaWindows.GetMainWindow());
            var result = paths != null ? paths.Select(path => new FileInfo(path)).ToArray() : null;
            return new OpenFileOutput(result);
        }

        /// <inheritdoc/>
        public async Task<SaveFileOutput> Handle(SaveFileInput request, CancellationToken cancellationToken)
        {
            var dialog = new SaveFileDialog();
            Initialize(dialog, request);
            var path = await dialog.ShowAsync(AvaloniaWindows.GetMainWindow());
            var result = new FileInfo(path);
            return new SaveFileOutput(result);
        }

        /// <inheritdoc/>
        public async Task<SelectFolderOutput> Handle(SelectFolderInput request, CancellationToken cancellationToken)
        {
            var dialog = new OpenFolderDialog { Directory = request.Path, Title = request.Title };
            var path = await dialog.ShowAsync(AvaloniaWindows.GetMainWindow());
            var result = path != null ? new DirectoryInfo(path) : null;
            return new SelectFolderOutput(result);
        }

        private static void Initialize<T>(FileDialog dialog, FileInput<T> input)
        {
            dialog.Title = input.Title;
            dialog.Filters = ParseFilters(input.Filter);
            dialog.Directory = input.Path;
        }

        private static List<FileDialogFilter> ParseFilters(string filter)
        {
            var result = new List<FileDialogFilter>();
            var parts = filter.Split('|').ToArray();
            for(var i=0; i<parts.Length; i+=2)
            {
                result.Add(new FileDialogFilter { Name = parts[i], Extensions = parts[i + 1].Split(',').ToList() });
            }
            return result;
        }
    }
}