using System.Threading;
using System.Threading.Tasks;
using Optional;
using Rethought.Commands.Parser;

namespace Rethought.Commands.Discord.Net.Sample
{
    public class ReverseTypeParser : ITypeParser<NormalizedDiscordContext, ReverseContext>
    {
        public Option<ReverseContext> Parse(NormalizedDiscordContext input)
        {
            return Option.Some(new ReverseContext(input.DiscordContext, input.NormalizedMessage));
        }
    }
}