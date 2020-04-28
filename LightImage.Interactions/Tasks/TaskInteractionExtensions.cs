using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class TaskInteractionExtensions
    {
        /// <summary>
        /// Handles an interaction representing a task and executes this task if the interaction was succesful.
        /// </summary>
        /// <typeparam name="T">Type of the task.</typeparam>
        /// <param name="service">The interaction service.</param>
        /// <param name="task">The task interaction.</param>
        /// <returns>Value indicating whether the task was executed.</returns>
        public static async Task<bool> Handle<T>(this IInteractionService service, T task)
            where T : IInteractionTask
        {
            var result = await service.Show<T, bool>(task);
            if (result)
            {
                await task.Execute();
            }
            return result;
        }
    }
}