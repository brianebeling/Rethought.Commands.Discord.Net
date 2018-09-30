using System;
using System.Collections.Generic;
using Rethought.Commands.Actions;
using Rethought.Commands.Builder;
using Rethought.Commands.Builder.Visitors;
using Rethought.Commands.Discord.Net.Prefix;
using Rethought.Commands.Discord.Net.TypeParsers;
using Rethought.Commands.Parser;

namespace Rethought.Commands.Discord.Net
{
    public static class AsyncFuncBuilderExtensions
    {
        public static AsyncFuncBuilder<TContext> WithPrefix<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            System.Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            System.Action<AsyncFuncBuilder<TContext2>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<TContext, TContext2>(
                    prefix,
                    message,
                    typeParser),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<NormalizedDiscordContext> WithPrefix(
            this AsyncFuncBuilder<NormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            System.Action<AsyncFuncBuilder<NormalizedDiscordContext>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<NormalizedDiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.NormalizedMessage,
                    (context, normalizedMessage) =>
                        new NormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<DiscordContext> WithPrefix(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            System.Action<AsyncFuncBuilder<NormalizedDiscordContext>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) =>
                        new NormalizedDiscordContext(context, normalizedMessage)),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<TContext> WithGroup<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            System.Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            List<System.Action<AsyncFuncBuilder<TContext2>>> configuration,
            System.Action<AsyncFuncBuilder<TContext2>> defaultAction)
        {
            configuration.Add(defaultAction);

            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<TContext, TContext2>(
                    prefix,
                    message,
                    typeParser),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<NormalizedDiscordContext> WithGroup(
            this AsyncFuncBuilder<NormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<System.Action<AsyncFuncBuilder<NormalizedDiscordContext>>> configuration,
            System.Action<AsyncFuncBuilder<NormalizedDiscordContext>> defaultAction)
        {
            configuration.Add(defaultAction);

            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<NormalizedDiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.NormalizedMessage,
                    (context, normalizedMessage) =>
                        new NormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        
        public static AsyncFuncBuilder<DiscordContext> WithGroup(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<System.Action<AsyncFuncBuilder<NormalizedDiscordContext>>> configuration,
            System.Action<AsyncFuncBuilder<NormalizedDiscordContext>> defaultAction)
        {
            configuration.Add(defaultAction);

            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) => new NormalizedDiscordContext(context, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<TContext> WithGroup<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            System.Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            List<System.Action<AsyncFuncBuilder<TContext2>>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<TContext, TContext2>(
                    prefix,
                    message,
                    typeParser),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<NormalizedDiscordContext> WithGroup(
            this AsyncFuncBuilder<NormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<System.Action<AsyncFuncBuilder<NormalizedDiscordContext>>> configuration)
        {
            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<NormalizedDiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.NormalizedMessage,
                    (context, normalizedMessage) =>
                        new NormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<DiscordContext> WithGroup(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<System.Action<AsyncFuncBuilder<NormalizedDiscordContext>>> configuration)
        {
            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, NormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) => new NormalizedDiscordContext(context, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }
    }
}