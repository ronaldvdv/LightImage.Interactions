using System.Threading.Tasks;
using LightImage.Interactions.Messages;

namespace LightImage.Interactions
{
    /// <summary>
    /// Extension methods for interactions with general messages and prompts for inputs.
    /// </summary>
    public static class MessageInteractionExtensions
    {
        /// <summary>
        /// Show a message and return the chosen button.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="options">Configuration for the interaction.</param>
        /// <returns>The button chosen as a result of the interaction.</returns>
        public static async Task<MessageButton> ShowMessage(this IInteractionService service, MessageOptions options)
        {
            var result = await service.Handle<MessageOptions, MessageResult>(options);
            return result.Button;
        }

        /// <summary>
        /// Show a message and return the chosen button.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title to be shown for the interaction.</param>
        /// <param name="message">Accompanying text for the interaction.</param>
        /// <param name="icon">Icon to be shown with the interaction.</param>
        /// <param name="button">Buttons to be shown as possible results for the interaction.</param>
        /// <returns>The button chosen as a result of the interaction.</returns>
        public static Task<MessageButton> ShowMessage(this IInteractionService service, string title, string message, MessageIcons icon = MessageIcons.Information, MessageButton button = MessageButton.Ok)
        {
            var options = new MessageOptions
            {
                Icon = icon,
                Title = title,
                Message = message,
                Buttons = button,
            };
            return service.ShowMessage(options);
        }

        /// <summary>
        /// Show a message and return whether the user chose the Yes or No button.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title to be shown for the interaction.</param>
        /// <param name="message">Accompanying text for the interaction.</param>
        /// <param name="icon">Icon to be shown with the interaction.</param>
        /// <returns>A value indicating whether the Yes button was chosen.</returns>
        public static async Task<bool> YesNo(this IInteractionService service, string title, string message, MessageIcons icon = MessageIcons.Question)
        {
            var result = await service.ShowMessage(title, message, icon, MessageButton.Yes | MessageButton.No);
            return result == MessageButton.Yes;
        }

        /// <summary>
        /// Show a message and return whether the user chose the Yes, No or Cancel button.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title to be shown for the interaction.</param>
        /// <param name="message">Accompanying text for the interaction.</param>
        /// <param name="icon">Icon to be shown with the interaction.</param>
        /// <returns>A value indicating whether the chosen button was Yes (true), No (false) or Cancel (null).</returns>
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