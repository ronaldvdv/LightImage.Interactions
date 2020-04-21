using System;

namespace LightImage.Interactions.Messages
{
    [Flags]
    public enum MessageButton
    {
        Ok = 1,
        Yes = 2,
        Cancel = 4,
        No = 8
    }
}