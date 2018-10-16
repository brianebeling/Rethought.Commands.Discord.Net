namespace Rethought.Commands.Discord.Net
{
    public class PrefixNormalizedDiscordContext
    {
        public PrefixNormalizedDiscordContext(DiscordContext discordContext, string prefixNormalizedMessage)
        {
            DiscordContext = discordContext;
            PrefixNormalizedMessage = prefixNormalizedMessage;
        }

        public DiscordContext DiscordContext { get; }
        public string PrefixNormalizedMessage { get; }
    }
}