using LightImage.Interactions.Messages;

namespace LightImage.Interactions.Prompts
{
    public class PromptResult
    {
        public PromptResult(string value, MessageButton button = MessageButton.Ok)
        {
            Value = value;
            Button = button;
        }

        public MessageButton Button { get; }
        public string Value { get; }
    }
}