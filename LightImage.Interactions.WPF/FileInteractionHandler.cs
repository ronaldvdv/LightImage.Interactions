using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LightImage.Interactions.Files;
using Microsoft.Win32;

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
        public Task<OpenFileOutput> Handle(OpenFileInput request, CancellationToken cancellationToken)
        {
            var dialog = new OpenFileDialog { Multiselect = request.MultiSelect, CheckFileExists = true };
            Initialize(dialog, request);
            var ok = dialog.ShowDialog();
            var result = ok == true ? dialog.FileNames.Select(f => new FileInfo(f)).ToArray() : null;
            return Task.FromResult(new OpenFileOutput(result));
        }

        /// <inheritdoc/>
        public Task<SaveFileOutput> Handle(SaveFileInput request, CancellationToken cancellationToken)
        {
            var dialog = new SaveFileDialog();
            Initialize(dialog, request);
            var ok = dialog.ShowDialog();
            var result = ok == true ? new FileInfo(dialog.FileName) : null;
            return Task.FromResult(new SaveFileOutput(result));
        }

        /// <inheritdoc/>
        public Task<SelectFolderOutput> Handle(SelectFolderInput request, CancellationToken cancellationToken)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = request.Title;
            dialog.UseDescriptionForTitle = true;
            dialog.ShowNewFolderButton = false;
            dialog.SelectedPath = request.Path;
            var ok = dialog.ShowDialog();
            var result = ok == System.Windows.Forms.DialogResult.OK ? new DirectoryInfo(dialog.SelectedPath) : null;
            return Task.FromResult(new SelectFolderOutput(result));
        }

        private static void Initialize<T>(FileDialog dialog, FileInput<T> input)
        {
            dialog.Title = input.Title;
            dialog.DefaultExt = input.DefaultExtension;
            dialog.Filter = input.Filter;
            dialog.FileName = input.Path;
        }
    }
}