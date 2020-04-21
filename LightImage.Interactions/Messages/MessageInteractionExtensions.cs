using LightImage.Interactions.Messages;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class MessageInteractionExtensions
    {
        public static async Task<MessageButton> ShowMessage(this IInteractionService service, MessageOptions options)
        {
            var result = await service.Show<MessageOptions, MessageResult>(options);
            return result.Button;
        }

        public static Task<MessageButton> ShowMessage(this IInteractionService service, string title, string message, MessageIcons icon = MessageIcons.Information, MessageButton button = MessageButton.Ok)
        {
            var options = new MessageOptions
            {
                Icon = icon,
                Title = title,
                Message = message,
                Buttons = button
            };
            return service.ShowMessage(options);
        }

        public static async Task<bool> YesNo(this IInteractionService service, string title, string message, MessageIcons icon = MessageIcons.Question)
        {
            var result = await service.ShowMessage(title, message, icon, MessageButton.Yes | MessageButton.No);
            return result == MessageButton.Yes;
        }

        public static async Task<bool?> YesNoCancel(this IInteractionService service, string title, string message, MessageIcons icon = MessageIcons.Question)
        {
            var result = await service.ShowMessage(title, message, icon, MessageButton.Yes | MessageButton.No | MessageButton.Cancel);
            switch (result)
            {
                case MessageButton.Yes:
                    return true;

                case MessageButton.No:
                    return false;

                case MessageButton.Cancel:
                default:
                    return null;
            }
        }
    }
}