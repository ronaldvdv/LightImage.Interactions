using System;

namespace LightImage.Interactions.Messages
{
    /// <summary>
    /// Buttons to be used for general interactions.
    /// </summary>
    [Flags]
    public enum MessageButton
    {
#pragma warning disable SA1602 // Enumeration items should be documented
        Ok = 1,
        Yes = 2,
        Cancel = 4,
        No = 8,
#pragma warning restore SA1602 // Enumeration items should be documented
    }
}