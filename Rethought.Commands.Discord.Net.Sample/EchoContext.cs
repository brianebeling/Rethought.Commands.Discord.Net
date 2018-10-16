using Optional;

namespace Rethought.Commands.Discord.Net.Sample
{
    public class EchoContext
    {
        public EchoContext(PrefixNormalizedDiscordContext prefixNormalizedDiscordContext, string content, Option<int> amountOption)
        {
            PrefixNormalizedDiscordContext = prefixNormalizedDiscordContext;
            Content = content;
            AmountOption = amountOption;
        }

        public PrefixNormalizedDiscordContext PrefixNormalizedDiscordContext { get; }

        public string Content { get; }

        public Option<int> AmountOption { get; }
    }
}