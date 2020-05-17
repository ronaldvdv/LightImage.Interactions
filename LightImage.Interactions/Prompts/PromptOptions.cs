using System;
using LightImage.Interactions.Messages;

namespace LightImage.Interactions.Prompts
{
    /// <summary>
    /// Input for interactions that prompt for some user input.
    /// </summary>
    public class PromptOptions : MessageOptionsBase, IInteractionInput<PromptResult>
    {
        /// <summary>
        /// Gets or sets a predicate that checks if a given input value is accepted.
        /// </summary>
        public Predicate<string> Predicate { get; set; }

        /// <summary>
        /// Gets or sets the initial value.
        /// </summary>
        public string Value { get; set; }
    }
}