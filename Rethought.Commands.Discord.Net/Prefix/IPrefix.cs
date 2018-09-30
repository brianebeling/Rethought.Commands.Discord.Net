using Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public interface IPrefix
    {
        Option<string> Normalize(string input);
    }
}