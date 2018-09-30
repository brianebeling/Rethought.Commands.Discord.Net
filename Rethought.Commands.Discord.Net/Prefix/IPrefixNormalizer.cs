using Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public interface IPrefixNormalizer
    {
        Option<string> Normalize(string message, ulong guildId);
    }
}