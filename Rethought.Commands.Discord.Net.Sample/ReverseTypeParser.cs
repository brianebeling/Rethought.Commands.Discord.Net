using Optional;
using Rethought.Commands.Parser;

namespace Rethought.Commands.Discord.Net.Sample
{
    public class ReverseTypeParser : ITypeParser<PrefixNormalizedDiscordContext, ReverseContext>
    {
        public Option<ReverseContext> Parse(PrefixNormalizedDiscordContext input)
        {
            return Option.Some(new ReverseContext(input.DiscordContext, input.PrefixNormalizedMessage));
        }
    }
}