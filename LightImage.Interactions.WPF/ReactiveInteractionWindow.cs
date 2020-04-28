using ReactiveUI;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LightImage.Interactions
{
    public abstract class ReactiveInteractionWindow<TViewModel> : ReactiveWindow<TViewModel>, IInteractionHandler<TViewModel, bool>
        where TViewModel : class, IInteractionInput<bool>
    {
        public async Task<bool> Handle(TViewModel request, CancellationToken cancellationToken)
        {
            ViewModel = request;
            var result = await this.ShowDialogAsync();
            return result == true;
        }
    }
}