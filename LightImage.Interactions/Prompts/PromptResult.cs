using LightImage.Interactions.Messages;

namespace LightImage.Interactions.Prompts
{
    /// <summary>
    /// Result class for prompt interactions.
    /// </summary>
    public class PromptResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromptResult"/> class.
        /// </summary>
        /// <param name="value">The entered value.</param>
        /// <param name="button">The chosen button.</param>
        public PromptResult(string value, MessageButton button = MessageButton.Ok)
        {
            Value = value;
            Button = button;
        }

        /// <summary>
        /// Gets the chosen button.
        /// </summary>
        public MessageButton Button { get; }

        /// <summary>
        /// Gets the entered value.
        /// </summary>
        public string Value { get; }
    }
}