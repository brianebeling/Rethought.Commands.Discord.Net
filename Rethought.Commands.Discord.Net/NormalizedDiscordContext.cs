namespace Rethought.Commands.Discord.Net
{
    public class NormalizedDiscordContext
    {
        public NormalizedDiscordContext(DiscordContext discordContext, string normalizedMessage)
        {
            DiscordContext = discordContext;
            NormalizedMessage = normalizedMessage;
        }

        public DiscordContext DiscordContext { get; }
        public string NormalizedMessage { get; }
    }

}