using LightImage.Interactions.Messages;
using System;

namespace LightImage.Interactions.Prompts
{
    public class PromptOptions : MessageOptions, IInteractionInput<PromptResult>
    {
        public Predicate<string> Predicate { get; set; }
        public string Value { get; set; }
    }
}