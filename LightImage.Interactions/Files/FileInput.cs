namespace LightImage.Interactions.Files
{
    /// <summary>
    /// Input for interactions related to selecting files.
    /// </summary>
    /// <typeparam name="TOutput">Type of output for the interaction.</typeparam>
    public abstract class FileInput<TOutput> : IInteractionInput<TOutput>
    {
        /// <summary>
        /// Gets or sets the extension to be used when no extension is chosen.
        /// </summary>
        public string DefaultExtension { get; set; }

        /// <summary>
        /// Gets or sets the filters available for showing a subset of existing files.
        /// </summary>
        /// <remarks>
        /// Filters are separated by a pipe symbol |; each filter has the form description|extensions like "Text files (*.txt)|*.txt".
        /// </remarks>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the path to the directory being shown.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the title for the interaction.
        /// </summary>
        public string Title { get; set; }
    }
}