using System;
using System.Collections.Generic;
using System.Text;

namespace Rethought.Commands.Discord.Net.Conditions
{
    public class IsOwner : IAsyncCondition<DiscordContext>
    {
        public async Task<bool> SatisfiedAsync(DiscordContext context)
        {
            var application = await context.Client.GetApplicationInfoAsync();
            if (context.User.Id != application.Owner.Id)
                return false;
            return true;
        }
    }
}
