namespace Rethought.Commands.Discord.Net.Sample
{
    public class ReverseContext
    {
        public ReverseContext(DiscordContext discordContext, string normalizedMessage)
        {
            DiscordContext = discordContext;
            NormalizedMessage = normalizedMessage;
        }

        public DiscordContext DiscordContext { get; }
        public string NormalizedMessage { get; }
    }
}