using Discord;

namespace Rethought.Commands.Discord.Net.Parser.UserTypeParser
{
    public struct UserTypeParserInput
    {
        public IGuild Guild { get; }
        public string Username { get; }

        public UserTypeParserInput(IGuild guild, string username)
        {
            Guild = guild;
            Username = username;
        }
    }
}