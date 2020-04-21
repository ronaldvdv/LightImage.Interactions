using MediatR;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public class InteractionService : IInteractionService
    {
        private readonly IMediator _mediator;

        public InteractionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TMessageOutput> Show<TMessageInput, TMessageOutput>(TMessageInput input)
            where TMessageInput : IInteractionInput<TMessageOutput>
        {
            return _mediator.Send(input);
        }
    }
}