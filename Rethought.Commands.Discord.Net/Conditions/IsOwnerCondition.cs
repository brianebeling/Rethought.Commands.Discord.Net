using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rethought.Commands.Conditions;

namespace Rethought.Commands.Discord.Net.Conditions
{
    public class IsOwnerCondition : IAsyncCondition<DiscordContext>
    {
        public async Task<bool> SatisfiedAsync(DiscordContext context)
        {
            var application = await context.Client.GetApplicationInfoAsync().ConfigureAwait(false);
            
            return context.User.Id == application.Owner.Id;
        }
    }
}
