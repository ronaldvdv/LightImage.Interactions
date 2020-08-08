using System;
using System.Globalization;

namespace LightImage.Interactions.Util
{
    /// <summary>
    /// Base class for parsers.
    /// </summary>
    /// <typeparam name="T">Type of value being parsed.</typeparam>
    public abstract class Parser<T>
    {
        private static Parser<T> _default;

        /// <summary>
        /// Gets the default parser for type <typeparamref name="T"/>.
        /// </summary>
        public static Parser<T> Default
        {
            get
            {
                if (_default == null)
                {
                    _default = GetDefaultParser();
                }

                return _default;
            }
        }

        /// <summary>
        /// Try and parse a string value.
        /// </summary>
        /// <param name="value">The value to be parsed.</param>
        /// <param name="result">The parser result.</param>
        /// <returns>Value indicating whether parsing was succesful.</returns>
        public abstract bool Parse(string value, out T result);

        private static Parser<T> GetDefaultParser()
        {
            var type = typeof(T);

            if (type == typeof(int?))
            {
                return (Parser<T>)(object)new OptionalIntParser();
            }

            if (type == typeof(int))
            {
                return (Parser<T>)(object)new IntParser();
            }

            if (type == typeof(float?))
            {
                return (Parser<T>)(object)new OptionalFloatParser();
            }

            if (type == typeof(float))
            {
                return (Parser<T>)(object)new FloatParser();
            }

            if (type == typeof(double?))
            {
                return (Parser<T>)(object)new OptionalDoubleParser();
            }

            if (type == typeof(double))
            {
                return (Parser<T>)(object)new DoubleParser();
            }

            if (type == typeof(string))
            {
                return (Parser<T>)(object)new StringParser();
            }

            throw new InvalidOperationException($"No default {nameof(Parser<T>)} could be found for type {type}");
        }

        private class DoubleParser : Parser<double>
        {
            public override bool Parse(string value, out double result)
            {
                return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            }
        }

        private class FloatParser : Parser<float>
        {
            public override bool Parse(string value, out float result)
            {
                return float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            }
        }

        private class IntParser : Parser<int>
        {
            public override bool Parse(string value, out int result)
            {
                return int.TryParse(value, out result);
            }
        }

        private class OptionalDoubleParser : Parser<double?>
        {
            public override bool Parse(string value, out double? result)
            {
                var success = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed);
                result = success ? parsed : default;
                return success;
            }
        }

        private class OptionalFloatParser : Parser<float?>
        {
            public override bool Parse(string value, out float? result)
            {
                var success = float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed);
                result = success ? parsed : default;
                return success;
            }
        }

        private class OptionalIntParser : Parser<int?>
        {
            public override bool Parse(string value, out int? result)
            {
                var success = int.TryParse(value, out var parsed);
                result = success ? parsed : default;
                return success;
            }
        }

        private class StringParser : Parser<string>
        {
            public override bool Parse(string value, out string result)
            {
                result = value;
                return true;
            }
        }
    }
}