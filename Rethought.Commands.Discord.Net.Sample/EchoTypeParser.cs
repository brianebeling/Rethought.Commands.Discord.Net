//using System.Threading;
//using System.Threading.Tasks;
//using Optional;
//using Rethought.Commands.Parser;

//namespace Rethought.Commands.Discord.Net.Sample
//{
//    public class EchoTypeParser : IAsyncTypeParser<NormalizedDiscordContext, EchoContext>
//    {


//        public async Task<Option<EchoContext>> ParseAsync(NormalizedDiscordContext input, CancellationToken cancellationToken)
//        {
//            if (input.NormalizedMessage != string.Empty)
//                return Option.Some(new EchoContext(input.DiscordContext, input.NormalizedMessage));

//            await input.DiscordContext.Channel.SendMessageAsync("Input string cannot be empty");

//            return default;
//        }
//    }
//}

