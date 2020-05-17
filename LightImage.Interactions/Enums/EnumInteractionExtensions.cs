using System;
using System.Threading.Tasks;
using LightImage.Interactions.Enums;

namespace LightImage.Interactions
{
    /// <summary>
    /// Extension methods for interactions that choose between enum members.
    /// </summary>
    public static class EnumInteractionExtensions
    {
        /// <summary>
        /// Handle an interaction for choosing one member of an enumeration type or NULL for cancellation.
        /// </summary>
        /// <typeparam name="TEnum">Type of enumeration with the options to choose from.</typeparam>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title to be shown for the interaction.</param>
        /// <param name="message">Accompanying text for the interaction.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<TEnum?> Show<TEnum>(this IInteractionService service, string title, string message)
                            where TEnum : struct, Enum
        {
            var vm = EnumViewModel.Create<TEnum>(title, message);
            var result = await service.Show<EnumViewModel, EnumMemberViewModel>(vm);
            return (TEnum?)result?.Value;
        }
    }
}