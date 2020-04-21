using System;
using System.Collections.Generic;
using System.Linq;

namespace LightImage.Interactions.Enums
{
    public class EnumViewModel : WindowOptions, IInteractionInput<EnumMemberViewModel>
    {
        private EnumViewModel(Type enumType)
        {
            var names = Enum.GetNames(enumType);
            Members = names.Select((name, i) => EnumMemberViewModel.Create(enumType, name, i == 0)).ToArray();
        }

        public IReadOnlyCollection<EnumMemberViewModel> Members { get; }

        public static EnumViewModel Create<TEnum>(string title, string message)
                    where TEnum : struct, Enum
        {
            return new EnumViewModel(typeof(TEnum))
            {
                Title = title,
                Message = message
            };
        }
    }
}