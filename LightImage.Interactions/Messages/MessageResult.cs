namespace LightImage.Interactions.Messages
{
    public class MessageResult
    {
        public MessageResult(MessageButton button)
        {
            Button = button;
        }

        public MessageButton Button { get; }
    }
}