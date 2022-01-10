using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace LightImage.Interactions
{
    internal static class AvaloniaWindows
    {
        public static Window GetMainWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return desktop.MainWindow;
            }

            throw new InvalidOperationException($"Cannot access MainWindow for a {Application.Current.ApplicationLifetime}");
        }
    }
}