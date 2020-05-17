using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LightImage.Interactions.Files;
using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;

namespace LightImage.Interactions
{
    /// <summary>
    /// Implementation of the <see cref="IInteractionService"/> for unit testing purposes.
    /// </summary>
    public class TestInteractionService : IInteractionService
    {
        private readonly Dictionary<Type, Func<object, object>> _results = new Dictionary<Type, Func<object, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TestInteractionService"/> class.
        /// </summary>
        public TestInteractionService()
        {
            Setup<MessageOptions, MessageResult>(() => new MessageResult(ShowResult));
            Setup<PromptOptions, PromptResult>(() => PromptResult);
            Setup<OpenFileInput, OpenFileOutput>(() => new OpenFileOutput(SelectFilesResult));
        }

        /// <summary>
        /// Gets or sets the result to be returned when a prompt interaction is handled.
        /// </summary>
        public PromptResult PromptResult { get; set; } = new PromptResult(string.Empty, MessageButton.Ok);

        /// <summary>
        /// Gets or sets the result to be returned when a file interaction is handled.
        /// </summary>
        public FileInfo[] SelectFilesResult { get; set; } = null;

        /// <summary>
        /// Gets or sets the result to be returned when a message interaction is handled.
        /// </summary>
        public MessageButton ShowResult { get; set; } = MessageButton.Ok;

        /// <summary>
        /// Register a function that generates results based on interaction input.
        /// </summary>
        /// <typeparam name="TInput">Type of interaction input.</typeparam>
        /// <typeparam name="TOutput">Type of interaction output.</typeparam>
        /// <param name="func">Function that generates results based on the given input.</param>
        public void Setup<TInput, TOutput>(Func<TInput, TOutput> func)
            where TInput : IInteractionInput<TOutput>
        {
            _results[typeof(TInput)] = o => func((TInput)o);
        }

        /// <summary>
        /// Register a function that generates results independent of interaction input.
        /// </summary>
        /// <typeparam name="TInput">Type of interaction input.</typeparam>
        /// <typeparam name="TOutput">Type of interaction output.</typeparam>
        /// <param name="func">Function that generates results.</param>
        public void Setup<TInput, TOutput>(Func<TOutput> func)
            where TInput : IInteractionInput<TOutput>
        {
            Setup<TInput, TOutput>(_ => func());
        }

        /// <summary>
        /// Register fixed interaciton output for a specific type of interaction input.
        /// </summary>
        /// <typeparam name="TInput">Type of interaction input.</typeparam>
        /// <typeparam name="TOutput">Type of interaction output.</typeparam>
        /// <param name="output">Fixed output for the given type of interaction.</param>
        public void Setup<TInput, TOutput>(TOutput output)
            where TInput : IInteractionInput<TOutput>
        {
            Setup<TInput, TOutput>(() => output);
        }

        /// <inheritdoc/>
        public Task<TMessageOutput> Show<TMessageInput, TMessageOutput>(TMessageInput input)
                                    where TMessageInput : IInteractionInput<TMessageOutput>
        {
            if (!_results.TryGetValue(typeof(TMessageInput), out var func))
            {
                throw new InvalidOperationException($"No test output defined for type '{typeof(TMessageOutput)}'");
            }

            var result = (TMessageOutput)func(input);
            return Task.FromResult(result);
        }
    }
}