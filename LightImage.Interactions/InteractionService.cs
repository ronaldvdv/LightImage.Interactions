using System.Threading.Tasks;
using MediatR;

namespace LightImage.Interactions
{
    /// <summary>
    /// Simple implementation of the <see cref="IInteractionService"/> contract using the mediator pattern.
    /// </summary>
    public class InteractionService : IInteractionService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionService"/> class.
        /// </summary>
        /// <param name="mediator">Mediator service for dispatching interaction view models.</param>
        public InteractionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc/>
        public Task<TMessageOutput> Show<TMessageInput, TMessageOutput>(TMessageInput input)
            where TMessageInput : IInteractionInput<TMessageOutput>
        {
            return _mediator.Send(input);
        }
    }
}