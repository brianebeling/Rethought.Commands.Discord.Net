using System;
using Optional;
using Rethought.Commands.Discord.Net.Prefix;
using Rethought.Commands.Parser;
using Rethought.Extensions.Optional;

namespace Rethought.Commands.Discord.Net.TypeParsers
{
    public class PrefixTypeParser<TInput, TOutput> : ITypeParser<TInput, TOutput>
    {
        private readonly System.Func<TInput, string> messageFunc;
        private readonly IPrefix prefix;
        private readonly Func<TInput, string, TOutput> typeParser;

        public PrefixTypeParser(
            IPrefix prefix,
            System.Func<TInput, string> messageFunc,
            Func<TInput, string, TOutput> typeParser)
        {
            this.prefix = prefix;
            this.messageFunc = messageFunc;
            this.typeParser = typeParser;
        }


        public Option<TOutput> Parse(TInput input)
        {
            return prefix.Normalize(messageFunc.Invoke(input)).TryGetValue(out var normalizedMessage)
                ? Option.Some(typeParser.Invoke(input, normalizedMessage))
                : Option.None<TOutput>();
        }
    }
}