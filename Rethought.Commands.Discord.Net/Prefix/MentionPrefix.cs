using System.Linq;
using Discord;
using Optional;

namespace Rethought.Commands.Discord.Net.Prefix
{
    public class MentionPrefix : IPrefix
    {
        private readonly IUser user;

        public MentionPrefix(IUser user)
        {
            this.user = user;
        }

        public Option<string> Normalize(string input)
        {
            var firstWord = input.Trim().Split(' ').First();

            return MentionUtils.TryParseUser(firstWord, out var id) && id == user.Id
                ? Option.Some(input.Length == firstWord.Length ? string.Empty : input.Remove(0, firstWord.Length + 1))
                : default;
        }
    }
}