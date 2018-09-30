using Discord;

namespace Rethought.Commands.Discord.Net
{
    public class DiscordContextFactory
    {
        private readonly IDiscordClient discordClient;

        public DiscordContextFactory(IDiscordClient discordClient)
        {
            this.discordClient = discordClient;
        }

        public DiscordContext Create(IUserMessage userMessage)
        {
            return new DiscordContext(discordClient, userMessage);
        }
    }
}