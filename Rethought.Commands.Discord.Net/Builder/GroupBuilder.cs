using System;
using System.Collections.Generic;
using Rethought.Commands.Builder;
using Rethought.Commands.Discord.Net.Prefix;

namespace Rethought.Commands.Discord.Net.Builder
{
    public class GroupBuilder<TContext>
    {
        public List<AsyncFuncBuilder<TContext>> AsyncFuncBuilders;

        public GroupBuilder<TContext> Add(IPrefix prefix, Action<AsyncFuncBuilder<TContext>> asyncFuncBuilder)
        {
            return this;
        }
    }
}