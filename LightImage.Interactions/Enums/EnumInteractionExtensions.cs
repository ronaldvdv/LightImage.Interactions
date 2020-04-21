using LightImage.Interactions.Enums;
using System;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class EnumInteractionExtensions
    {
        public static async Task<TEnum?> Show<TEnum>(this IInteractionService service, string title, string message)
                            where TEnum : struct, Enum
        {
            var vm = EnumViewModel.Create<TEnum>(title, message);
            var result = await service.Show<EnumViewModel, EnumMemberViewModel>(vm);
            return (TEnum?)result?.Value;
        }
    }
}