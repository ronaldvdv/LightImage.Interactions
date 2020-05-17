using System.Threading.Tasks;

namespace LightImage.Interactions
{
    /// <summary>
    /// Service for handling user interactions for view models.
    /// </summary>
    public interface IInteractionService
    {
        /// <summary>
        /// Handle a specific interaction.
        /// </summary>
        /// <typeparam name="TInput">Type of input for the interaction.</typeparam>
        /// <typeparam name="TOutput">Type of output of the interaction.</typeparam>
        /// <param name="input">Input for the interaction.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<TOutput> Handle<TInput, TOutput>(TInput input)
            where TInput : IInteractionInput<TOutput>;
    }
}