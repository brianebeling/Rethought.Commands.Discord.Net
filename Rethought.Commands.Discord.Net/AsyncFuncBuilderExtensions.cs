using System;
using System.Collections.Generic;
using Rethought.Commands.Actions;
using Rethought.Commands.Builder;
using Rethought.Commands.Discord.Net.Prefix;
using Rethought.Commands.Discord.Net.TypeParsers;

namespace Rethought.Commands.Discord.Net
{
    public static class AsyncFuncBuilderExtensions
    {
        public static AsyncFuncBuilder<TContext> WithPrefix<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            Action<AsyncFuncBuilder<TContext2>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<TContext, TContext2>(
                    prefix,
                    message,
                    typeParser),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<PrefixNormalizedDiscordContext> WithPrefix(
            this AsyncFuncBuilder<PrefixNormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<PrefixNormalizedDiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.PrefixNormalizedMessage,
                    (context, normalizedMessage) =>
                        new PrefixNormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<DiscordContext> WithPrefix(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) =>
                        new PrefixNormalizedDiscordContext(context, normalizedMessage)),
                configuration);

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<TContext> WithGroup<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            List<Action<AsyncFuncBuilder<TContext2>>> configuration,
            Action<AsyncFuncBuilder<TContext2>> defaultAction)
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

        public static AsyncFuncBuilder<PrefixNormalizedDiscordContext> WithGroup(
            this AsyncFuncBuilder<PrefixNormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>>> configuration,
            Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>> defaultAction)
        {
            configuration.Add(defaultAction);

            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<PrefixNormalizedDiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.PrefixNormalizedMessage,
                    (context, normalizedMessage) =>
                        new PrefixNormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }


        public static AsyncFuncBuilder<DiscordContext> WithGroup(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>>> configuration,
            Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>> defaultAction)
        {
            configuration.Add(defaultAction);

            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) => new PrefixNormalizedDiscordContext(context, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<TContext> WithGroup<TContext, TContext2>(
            this AsyncFuncBuilder<TContext> asyncFuncBuilder,
            IPrefix prefix,
            Func<TContext, string> message,
            Func<TContext, string, TContext2> typeParser,
            List<Action<AsyncFuncBuilder<TContext2>>> configuration)
        {
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<TContext, TContext2>(
                    prefix,
                    message,
                    typeParser),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<PrefixNormalizedDiscordContext> WithGroup(
            this AsyncFuncBuilder<PrefixNormalizedDiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>>> configuration)
        {
            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<PrefixNormalizedDiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.PrefixNormalizedMessage,
                    (context, normalizedMessage) =>
                        new PrefixNormalizedDiscordContext(context.DiscordContext, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }

        public static AsyncFuncBuilder<DiscordContext> WithGroup(
            this AsyncFuncBuilder<DiscordContext> asyncFuncBuilder,
            IPrefix prefix,
            List<Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>>> configuration)
        {
            // TODO: Optimize by making a new type parser for the specific use case
            asyncFuncBuilder.WithAdapter(
                new PrefixTypeParser<DiscordContext, PrefixNormalizedDiscordContext>(
                    prefix,
                    context => context.Message.Content,
                    (context, normalizedMessage) => new PrefixNormalizedDiscordContext(context, normalizedMessage)),
                group => group.Any(configuration, result => result == Result.Completed || result == Result.Aborted));

            return asyncFuncBuilder;
        }
    }
}