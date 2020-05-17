using MediatR;

namespace LightImage.Interactions
{
    /// <summary>
    /// Contract for classes that handle interactions.
    /// </summary>
    /// <typeparam name="TInput">Type of interaction input.</typeparam>
    /// <typeparam name="TOutput">Type of interaction output.</typeparam>
    public interface IInteractionHandler<in TInput, TOutput>
        : IRequestHandler<TInput, TOutput>
        where TInput : IInteractionInput<TOutput>
    {
    }
}