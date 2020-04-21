using System;
using System.Globalization;

namespace LightImage.Interactions.Util
{
    public abstract class Parser<T>
    {
        private static Parser<T> _default;

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

        public abstract bool Parse(string value, out T result);

        private static Parser<T> GetDefaultParser()
        {
            var type = typeof(T);

            if (type == typeof(int?))
                return (Parser<T>)(object)new OptionalIntParser();

            if (type == typeof(int))
                return (Parser<T>)(object)new IntParser();

            if (type == typeof(float?))
                return (Parser<T>)(object)new OptionalFloatParser();

            if (type == typeof(float))
                return (Parser<T>)(object)new FloatParser();

            if (type == typeof(string))
                return (Parser<T>)(object)new StringParser();

            throw new InvalidOperationException($"No default {nameof(Parser<T>)} could be found for type {type}");
        }

        private class FloatParser : Parser<float>
        {
            public override bool Parse(string value, out float result) => float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        private class IntParser : Parser<int>
        {
            public override bool Parse(string value, out int result) => int.TryParse(value, out result);
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