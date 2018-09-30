using Optional;
using Rethought.Extensions.Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public class PrefixNormalizer : IPrefixNormalizer
    {
        private readonly IPrefixRepository prefixRepository;

        public PrefixNormalizer(IPrefixRepository prefixRepository)
        {
            this.prefixRepository = prefixRepository;
        }

        public Option<string> Normalize(string message, ulong guildId)
        {
            if (prefixRepository.Get(guildId).TryGetValue(out var prefixList))
            {
                foreach (var prefix in prefixList)
                {
                    var normalizedMessage = prefix.Normalize(message);

                    if (normalizedMessage.HasValue)
                    {
                        return normalizedMessage;
                    }
                }
            }

            return default;
        }
    }
}