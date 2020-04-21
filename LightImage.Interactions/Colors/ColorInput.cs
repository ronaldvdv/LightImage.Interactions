namespace LightImage.Interactions.Colors
{
    public class ColorInput : ColorMessageBase, IInteractionInput<ColorResult>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the result should be updated during the interaction.
        /// </summary>
        /// <remarks>
        /// Set this property to TRUE to allow live preview of color changes.
        /// </remarks>
        public bool EnableChangedEvent { get; set; } = false;

        /// <summary>
        /// Gets or sets the title for a color picker.
        /// </summary>
        public string Title { get; set; } = "Select color";

        /// <summary>
        /// Gets or sets a value indicating whether the opacity channel should be chosen too.
        /// </summary>
        public bool UseOpacity { get; set; } = true;

        /// <summary>
        /// Provides a callback for handling changes during the interaction.
        /// </summary>
        public virtual void OnChanged()
        {
        }
    }
}