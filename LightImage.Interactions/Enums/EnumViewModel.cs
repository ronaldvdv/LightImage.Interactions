using System;
using System.Collections.Generic;
using System.Linq;

namespace LightImage.Interactions.Enums
{
    /// <summary>
    /// View model describing an enumeration of options.
    /// </summary>
    public class EnumViewModel : WindowOptions, IInteractionInput<EnumMemberViewModel>
    {
        private EnumViewModel(Type enumType)
        {
            var names = Enum.GetNames(enumType);
            Members = names.Select((name, i) => EnumMemberViewModel.Create(enumType, name, i == 0)).ToArray();
        }

        /// <summary>
        /// Gets the collection of enumeration members.
        /// </summary>
        public IReadOnlyCollection<EnumMemberViewModel> Members { get; }

        /// <summary>
        /// Create an <see cref="EnumViewModel"/> instance based on the <typeparamref name="TEnum"/> type parameter.
        /// </summary>
        /// <typeparam name="TEnum">Type of enumeration.</typeparam>
        /// <param name="title">Title to be shown for the interaction.</param>
        /// <param name="message">Accompanying text for the interaction.</param>
        /// <returns>The new view model.</returns>
        public static EnumViewModel Create<TEnum>(string title, string message)
                    where TEnum : struct, Enum
        {
            return new EnumViewModel(typeof(TEnum))
            {
                Title = title,
                Message = message,
            };
        }
    }
}