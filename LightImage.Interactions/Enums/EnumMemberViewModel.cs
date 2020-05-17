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

        /// <summary>
        /// Create a new instance of a <see cref="EnumMemberViewModel"/>.
        /// </summary>
        /// <param name="enumType">Type of enumeration.</param>
        /// <param name="memberName">Name of the enumeration member to be represented by the viewmodel.</param>
        /// <param name="selected">Value indicating whether the member is currently selected.</param>
        /// <returns>The view model representing the member.</returns>
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