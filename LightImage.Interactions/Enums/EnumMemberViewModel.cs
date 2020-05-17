using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LightImage.Interactions.Enums
{
    /// <summary>
    /// View model representing an enumeration member.
    /// </summary>
    public class EnumMemberViewModel
    {
        private EnumMemberViewModel(string text, string description, object value)
        {
            Text = text;
            Description = description;
            Value = value;
        }

        /// <summary>
        /// Gets the description for the enumeration member.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the currently chosen enumeration member during an interaction.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets the main text identifying the enumeration member.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the value associated with this enumeration member.
        /// </summary>
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