using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Optional;
using Rethought.Commands.Parser;

namespace Rethought.Commands.Discord.Net.Parser.UserTypeParser
{
    public class AsyncUserTypeParser : IAsyncAbortableTypeParser<UserTypeParserInput, IGuildUser>
    {
        public async Task<(bool Completed, Option<IGuildUser> Result)> ParseAsync(
            UserTypeParserInput input,
            CancellationToken cancellationToken)
        {
            var result = await ParseByMentionAsync(input.Guild, input.Username).ConfigureAwait(false);

            if (result != null) return default;

            if (ulong.TryParse(input.Username, out var id))
            {
                result = await ParseByIdAsync(input.Guild, id).ConfigureAwait(false);

                if (result != null) return (true, Option.Some(result));

                result = await ParseByUsernameAsync(input.Guild, input.Username).ConfigureAwait(false);

                if (result != null)
                    return (true, Option.Some(result));

                return default;
            }

            return (true, Option.Some(await ParseByUsernameAsync(input.Guild, input.Username).ConfigureAwait(false)));
        }

        // TODO Apply Option Pattern
        private static async Task<IGuildUser> ParseByMentionAsync(IGuild guild, string input)
        {
            if (MentionUtils.TryParseUser(input, out var id))
            {
                return await guild.GetUserAsync(id).ConfigureAwait(false);
            }

            return null;
        }

        // TODO Apply Option Pattern
        private static Task<IGuildUser> ParseByIdAsync(IGuild guild, ulong input)
        {
            return guild.GetUserAsync(input);
        }

        // TODO Apply Option Pattern
        private static async Task<IGuildUser> ParseByUsernameAsync(IGuild guild, string input)
        {
            var users = await guild.GetUsersAsync().ConfigureAwait(false);

            return users.FirstOrDefault(x => x.Username == input);
        }
    }
}