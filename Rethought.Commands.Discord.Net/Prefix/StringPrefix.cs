using System;
using System.Linq;
using Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public class StringPrefix : IPrefix
    {
        private readonly string value;

        public StringPrefix(string value)
        {
            this.value = value;
        }

        public Option<string> Normalize(string input)
        {
            var firstWord = input.Trim().Split(' ').First();

            return string.Equals(firstWord, value, StringComparison.InvariantCultureIgnoreCase)
                ? (input.Length == firstWord.Length
                    ? Option.Some(string.Empty)
                    : Option.Some(input.Remove(0, firstWord.Length + 1)))
                : default;
        }
    }
}