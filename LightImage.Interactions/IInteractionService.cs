using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public interface IInteractionService
    {
        Task<TOutput> Show<TInput, TOutput>(TInput input)
            where TInput : IInteractionInput<TOutput>;
    }
}