using MediatR;

namespace LightImage.Interactions
{
    public interface IInteractionHandler<in TInput, TOutput>
        : IRequestHandler<TInput, TOutput>
        where TInput : IInteractionInput<TOutput>
    {
    }
}