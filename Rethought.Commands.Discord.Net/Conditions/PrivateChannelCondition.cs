using Rethought.Commands.Conditions;

namespace Rethought.Commands.Discord.Net.Conditions
{
    public class PrivateChannelCondition : ICondition<DiscordContext>
    {
        public bool Satisfied(DiscordContext context)
        {
            return context.IsPrivateChannel;
        }
    }
}