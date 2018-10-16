using Discord.WebSocket;
using Rethought.Commands.Conditions;

namespace Rethought.Commands.Discord.Net.Conditions
{
    public class NsfwChannelCondition : ICondition<DiscordContext>
    {
        public bool Satisfied(DiscordContext context)
        {
            return context.Channel is SocketTextChannel socketTextChannel && socketTextChannel.IsNsfw;
        }
    }
}