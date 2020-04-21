using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LightImage.Interactions.Enums
{
    public class EnumMemberViewModel
    {
        public EnumMemberViewModel(string text, string description, object value)
        {
            Text = text;
            Description = description;
            Value = value;
        }

        public string Description { get; }

        public bool IsSelected { get; set; }
        public string Text { get; }

        public object Value { get; }

        public static EnumMemberViewModel Create(Type enumType, string memberName, bool selected)
        {
            var value = Enum.Parse(enumType, memberName);
            var member = enumType.GetMember(memberName).Single();
            var description = member.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
            var name = member.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? memberName;
            return new EnumMemberViewModel(name, description, value) { IsSelected = selected };
        }
    }
}