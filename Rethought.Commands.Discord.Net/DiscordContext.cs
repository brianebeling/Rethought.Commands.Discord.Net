using Discord;

namespace Rethought.Commands.Discord.Net
{
    public class DiscordContext
    {
        internal DiscordContext(IDiscordClient client, IUserMessage msg)
        {
            Client = client;
            Guild = (msg.Channel as IGuildChannel)?.Guild;
            Channel = msg.Channel;
            User = msg.Author;
            Message = msg;
        }


        public IMessageChannel Channel { get; }

        public IDiscordClient Client { get; }

        public IGuild Guild { get; }

        // Kind of out of place here
        public bool IsPrivateChannel => Channel is IPrivateChannel;

        public IUserMessage Message { get; }

        public IUser User { get; }
    }
}