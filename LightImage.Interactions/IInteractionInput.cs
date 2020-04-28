using MediatR;

namespace LightImage.Interactions
{
    public interface IInteractionInput<out TOutput>
            : IRequest<TOutput>
    {
    }
}