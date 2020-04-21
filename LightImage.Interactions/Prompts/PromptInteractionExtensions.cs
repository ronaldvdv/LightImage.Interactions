using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;
using LightImage.Interactions.Util;
using System;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class PromptInteractionExtensions
    {
        public static async Task<string> Input(this IInteractionService service, string title, string message, string defaultValue, MessageIcons icon = MessageIcons.Question, Predicate<string> predicate = null)
        {
            var result = await Prompt(service, title, message, defaultValue, icon, null, null, predicate);
            return result.Success ? result.Value : null;
        }

        public static async Task<int?> Input(this IInteractionService service, string title, string message, int defaultValue, MessageIcons icon = MessageIcons.Question, Predicate<int> predicate = null)
        {
            var result = await Prompt(service, title, message, defaultValue, icon, null, null, predicate);
            return result.Success ? (int?)result.Value : null;
        }

        public static async Task<InputResult<T>> Prompt<T>(this IInteractionService service, string title, string message, T defaultValue, MessageIcons icon = MessageIcons.Question, Parser<T> parser = null, Formatter<T> formatter = null, Predicate<T> predicate = null)
        {
            predicate = predicate ?? new Predicate<T>(_ => true);
            formatter = formatter ?? new Formatter<T>(x => x?.ToString());
            parser = parser ?? Parser<T>.Default;

            var stringPredicate = new Predicate<string>(s => parser.Parse(s, out var value) && predicate(value));
            var options = new PromptOptions
            {
                Title = title,
                Message = message,
                Value = formatter(defaultValue),
                Predicate = stringPredicate,
                Icon = icon,
                Buttons = MessageButton.Ok | MessageButton.Cancel
            };

            var result = await service.Prompt(options);

            if (result.Button == MessageButton.Ok)
            {
                var success = parser.Parse(result.Value, out var output);
                return new InputResult<T> { Success = success, Value = output };
            }
            else
            {
                return InputResult<T>.NoSuccess;
            }
        }

        public static Task<PromptResult> Prompt(this IInteractionService service, PromptOptions options)
        {
            return service.Show<PromptOptions, PromptResult>(options);
        }

        public struct InputResult<T>
        {
            public InputResult(T result)
            {
                Value = result;
                Success = true;
            }

            public static InputResult<T> NoSuccess { get; } = new InputResult<T> { Value = default, Success = false };
            public bool Success { get; set; }
            public T Value { get; set; }
        }
    }
}