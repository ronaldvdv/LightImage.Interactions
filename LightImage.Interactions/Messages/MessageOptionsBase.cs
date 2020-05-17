namespace LightImage.Interactions.Messages
{
    /// <summary>
    /// Base class for interactions that use buttons and an icon.
    /// </summary>
    public class MessageOptionsBase : WindowOptions
    {
        /// <summary>
        /// Gets or sets the buttons to be shown.
        /// </summary>
        public MessageButton Buttons { get; set; }

        /// <summary>
        /// Gets or sets the icon to be shown.
        /// </summary>
        public MessageIcons Icon { get; set; }
    }
}