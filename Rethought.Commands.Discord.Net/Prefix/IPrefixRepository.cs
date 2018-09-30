using System.Collections.Generic;
using Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public interface IPrefixRepository
    {
        Option<IList<IPrefix>> Get(ulong guildId);
    }
}