using LightImage.Interactions.Colors;
using LightImage.Interactions.Files;
using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace LightImage.Interactions
{
    public class TestInteractionService : IInteractionService
    {
        #region Test results

        public PromptResult PromptResult { get; set; } = new PromptResult(string.Empty, MessageButton.Ok);
        public ColorResult SelectColorResult { get; set; } = new ColorResult { Red = 50, Green = 75, Blue = 100, Opacity = 100, Success = true };
        public FileInfo[] SelectFilesResult { get; set; } = null;
        public MessageButton ShowResult { get; set; } = MessageButton.Ok;

        #endregion Test results

        #region Methods

        public void Setup<TInput, TOutput>(Func<TInput, TOutput> func) where TInput : IInteractionInput<TOutput>
        {
            _results[typeof(TInput)] = o => func((TInput)o);
        }

        public void Setup<TInput, TOutput>(Func<TOutput> func) where TInput : IInteractionInput<TOutput>
        {
            Setup<TInput, TOutput>(_ => func());
        }

        public void Setup<TInput, TOutput>(TOutput output) where TInput : IInteractionInput<TOutput>
        {
            Setup<TInput, TOutput>(() => output);
        }

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

        #endregion Methods

        private readonly Dictionary<Type, Func<object, object>> _results = new Dictionary<Type, Func<object, object>>();

        public TestInteractionService()
        {
            Setup<MessageOptions, MessageResult>(() => new MessageResult(ShowResult));
            Setup<PromptOptions, PromptResult>(() => PromptResult);
            Setup<SelectFileInput, SelectFileOutput>(() => new SelectFileOutput(SelectFilesResult));
            Setup<ColorInput, ColorResult>(() => SelectColorResult);
        }
    }
}