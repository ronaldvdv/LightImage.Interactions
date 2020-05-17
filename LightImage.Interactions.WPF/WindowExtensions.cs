using System.Threading.Tasks;

namespace System.Windows
{
    /// <summary>
    /// Extension methods for windows.
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Shows a window in dialog mode and returns a task that completes when the window has been closed.
        /// </summary>
        /// <param name="window">The window to be shown.</param>
        /// <returns>Task completing when the window closes and returning the <see cref="Window.DialogResult"/> value.</returns>
        public static Task<bool?> ShowDialogAsync(this Window window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("self");
            }

            var completion = new TaskCompletionSource<bool?>();
            window.Dispatcher.BeginInvoke(new Action(() => completion.SetResult(window.ShowDialog())));
            return completion.Task;
        }
    }
}