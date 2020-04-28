using System.Threading.Tasks;

namespace System.Windows
{
    public static class WindowExtensions
    {
        public static Task<bool?> ShowDialogAsync(this Window self)
        {
            if (self == null) throw new ArgumentNullException("self");

            var completion = new TaskCompletionSource<bool?>();
            self.Dispatcher.BeginInvoke(new Action(() => completion.SetResult(self.ShowDialog())));
            return completion.Task;
        }
    }
}