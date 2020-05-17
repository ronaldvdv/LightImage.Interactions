using System;
using System.Threading.Tasks;
using LightImage.Interactions.Messages;
using LightImage.Interactions.Prompts;
using LightImage.Interactions.Util;

namespace LightImage.Interactions
{
    /// <summary>
    /// Extension methods for interactions that ask for user input.
    /// </summary>
    public static class PromptInteractionExtensions
    {
        /// <summary>
        /// Ask for user input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title for the interaction.</param>
        /// <param name="message">Message to be shown.</param>
        /// <param name="defaultValue">Default value to be shown.</param>
        /// <param name="icon">Icon to be shown.</param>
        /// <param name="predicate">Predicate to test if a given input value is acceptable.</param>
        /// <returns>The entered value, or NULL if canceled.</returns>
        public static async Task<string> Input(this IInteractionService service, string title, string message, string defaultValue, MessageIcons icon = MessageIcons.Question, Predicate<string> predicate = null)
        {
            var result = await Prompt(service, title, message, defaultValue, icon, null, null, predicate);
            return result.Success ? result.Value : null;
        }

        /// <summary>
        /// Ask for numeric user input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title for the interaction.</param>
        /// <param name="message">Message to be shown.</param>
        /// <param name="defaultValue">Default value to be shown.</param>
        /// <param name="icon">Icon to be shown.</param>
        /// <param name="predicate">Predicate to test if a given input value is acceptable.</param>
        /// <returns>The entered value, or NULL if canceled.</returns>
        public static async Task<int?> Input(this IInteractionService service, string title, string message, int defaultValue, MessageIcons icon = MessageIcons.Question, Predicate<int> predicate = null)
        {
            var result = await Prompt(service, title, message, defaultValue, icon, null, null, predicate);
            return result.Success ? (int?)result.Value : null;
        }

        /// <summary>
        /// Ask for strongly-typed user input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="title">Title for the interaction.</param>
        /// <param name="message">Message to be shown.</param>
        /// <param name="defaultValue">Default value to be shown.</param>
        /// <param name="icon">Icon to be shown.</param>
        /// <param name="parser">Service for parsing string input to the desired type.</param>
        /// <param name="formatter">Service for formatting typed values as strings.</param>
        /// <param name="predicate">Predicate to test if a given input value is acceptable.</param>
        /// <returns>The entered value.</returns>
        /// <typeparam name="T">Type of result input.</typeparam>
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
                Buttons = MessageButton.Ok | MessageButton.Cancel,
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

        /// <summary>
        /// Ask for user input using a view model to specify interaction input.
        /// </summary>
        /// <param name="service">The interaction service.</param>
        /// <param name="options">Options for the interaction.</param>
        /// <returns>The result of the interaction.</returns>
        public static Task<PromptResult> Prompt(this IInteractionService service, PromptOptions options)
        {
            return service.Handle<PromptOptions, PromptResult>(options);
        }

        /// <summary>
        /// Structure for returning prompt results.
        /// </summary>
        /// <typeparam name="T">Type of result value.</typeparam>
        public struct InputResult<T>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InputResult{T}"/> struct.
            /// </summary>
            /// <param name="result">The result value.</param>
            public InputResult(T result)
            {
                Value = result;
                Success = true;
            }

            /// <summary>
            /// Gets the result for representing "no success".
            /// </summary>
            public static InputResult<T> NoSuccess { get; } = new InputResult<T> { Value = default, Success = false };

            /// <summary>
            /// Gets or sets a value indicating whether the interaction was succesful.
            /// </summary>
            public bool Success { get; set; }

            /// <summary>
            /// Gets or sets the result value.
            /// </summary>
            public T Value { get; set; }
        }
    }
}