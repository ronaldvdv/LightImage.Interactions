namespace LightImage.Interactions.Files
{

    /// <summary>
    /// Input for an interaction to select a folder.
    /// </summary>
    public class SelectFolderInput : IInteractionInput<SelectFolderOutput>
    {
        /// <summary>
        /// Gets or sets the path to the directory being shown initially.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the title for the interaction.
        /// </summary>
        public string Title { get; set; }
    }
}