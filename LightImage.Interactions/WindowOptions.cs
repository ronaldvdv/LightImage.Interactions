namespace LightImage.Interactions
{
    /// <summary>
    /// Base class for view models representing input to interactions that use a window.
    /// </summary>
    public class WindowOptions
    {
        /// <summary>
        /// Gets or sets the main message to be displayed.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the title for the interaction.
        /// </summary>
        public string Title { get; set; }
    }
}