using System.Threading;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;

namespace LightImage.Interactions
{
    /// <summary>
    /// Base class for <see cref="ReactiveWindow{TViewModel}"/> classes that handle interactions.
    /// </summary>
    /// <typeparam name="TViewModel">Type of interaction input view model.</typeparam>
    public abstract class ReactiveInteractionWindow<TViewModel> : ReactiveWindow<TViewModel>, IInteractionHandler<TViewModel, bool>
        where TViewModel : class, IInteractionInput<bool>
    {
        public ReactiveInteractionWindow(IInteractionWindowBuilder builder)
        {
            builder.Setup(this);
        }

        /// <inheritdoc/>
        public async Task<bool> Handle(TViewModel request, CancellationToken cancellationToken)
        {
            ViewModel = request;
            var result = await ShowDialog<bool?>(AvaloniaWindows.GetMainWindow());
            return result == true;
        }
    }
}