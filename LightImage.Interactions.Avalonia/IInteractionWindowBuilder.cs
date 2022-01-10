using Avalonia.Controls;

namespace LightImage.Interactions
{
    /// <summary>
    /// Service for setting up interaction windows.
    /// </summary>
    public interface IInteractionWindowBuilder
    {
        /// <summary>
        /// Callback used when a <see cref="OwnedInteractionWindow"/> or <see cref="OwnedReactiveInteractionWindow"/> is instantiated.
        /// </summary>
        /// <param name="window"></param>
        void Setup(Window window);
    }
}