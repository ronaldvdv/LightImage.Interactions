using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LightImage.Interactions
{
    public abstract class InteractionWindow<TInput, TOutput> : Window, IInteractionHandler<TInput, TOutput>
        where TInput : IInteractionInput<TOutput>
    {
        public InteractionWindow(IInteractionWindowBuilder builder)
        {
            builder.Setup(this);
        }

        public async Task<TOutput> Handle(TInput input, CancellationToken cancellationToken)
        {
            SetInput(input);
            await this.ShowDialogAsync();
            var result = GetOutput();
            return result;
        }

        protected abstract TOutput GetOutput();

        protected abstract void SetInput(TInput request);
    }
}