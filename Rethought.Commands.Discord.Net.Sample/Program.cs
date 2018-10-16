using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Rethought.Commands.Builder;
using Rethought.Commands.Discord.Net.Prefix;
using Rethought.Commands.Parser.Auto;
using Rethought.Extensions.Optional;

namespace Rethought.Commands.Discord.Net.Sample
{
    public static class Program
    {
        public static async Task Main()
        {
            var discordSocketClient = new DiscordSocketClient(
                new DiscordSocketConfig
                {
                    AlwaysDownloadUsers = true, LogLevel = LogSeverity.Verbose, DefaultRetryMode = RetryMode.AlwaysRetry
                });

            discordSocketClient.Log += message =>
            {
                Console.WriteLine(message);
                return Task.CompletedTask;
            };

            await discordSocketClient.LoginAsync(TokenType.Bot, "TOKEN").ConfigureAwait(false);
            await discordSocketClient.StartAsync().ConfigureAwait(false);

            var discordContextFactory = new DiscordContextFactory(discordSocketClient);

            var asyncFuncBuilder = AsyncFuncBuilder<DiscordContext>.Create();

            discordSocketClient.Ready += () =>
            {
                var autoBuilderFactory = new AutoBuilderFactory(TypeParserDictionaryFactory.CreateDefault());

                asyncFuncBuilder
                    .WithCondition(context => !context.User.IsBot)
                    .WithCondition(context => !context.IsPrivateChannel)
                    .WithPrefix(
                        new MentionPrefix(discordSocketClient.CurrentUser),
                        root =>
                            root.WithGroup(
                                new StringPrefix("misc"),
                                new List<Action<AsyncFuncBuilder<PrefixNormalizedDiscordContext>>>
                                {
                                    echo => echo.WithPrefix(
                                        new StringPrefix("echo"),
                                        command => command.WithAdapter(
                                            autoBuilderFactory.Create()
                                                .Build<PrefixNormalizedDiscordContext, EchoContext>(
                                                    input => input.PrefixNormalizedMessage),
                                            CreateEchoConfiguration())),

                                    reverse => reverse.WithPrefix(
                                        new StringPrefix("reverse"),
                                        command => command.WithAdapter(
                                            new ReverseTypeParser(),
                                            CreateReverseConfiguration()))
                                },
                                fallback => fallback.WithAsyncFunc(
                                    async context =>
                                    {
                                        await context.DiscordContext.Channel.SendMessageAsync(
                                                "Sorry, I don't know that command!")
                                            .ConfigureAwait(false);
                                    })));

                var asyncAction = asyncFuncBuilder.Build();

                discordSocketClient.MessageReceived += async message =>
                {
                    if (message is IUserMessage userMessage)
                    {
                        var result = await asyncAction.InvokeAsync(
                                discordContextFactory.Create(userMessage),
                                CancellationToken.None)
                            .ConfigureAwait(false);
                    }
                };

                return Task.CompletedTask;
            };

            await Task.Delay(-1).ConfigureAwait(false);
        }

        private static Action<AsyncFuncBuilder<ReverseContext>> CreateReverseConfiguration()
        {
            return reverse => reverse
                .WithAsyncFunc(
                    async reverseContext =>
                    {
                        await reverseContext.DiscordContext.Channel.SendMessageAsync(
                                new string(reverseContext.NormalizedMessage.Reverse().ToArray()))
                            .ConfigureAwait(false);
                    });
        }


        private static Action<AsyncFuncBuilder<EchoContext>> CreateEchoConfiguration()
        {
            return echo => echo
                .WithAsyncFunc(
                    async echoContext =>
                    {
                        if (echoContext.AmountOption.TryGetValue(out var amount))
                        {
                            await echoContext.PrefixNormalizedDiscordContext.DiscordContext.Channel.SendMessageAsync(
                                    string.Concat(Enumerable.Repeat(echoContext.Content, amount)))
                                .ConfigureAwait(false);
                        }

                        await echoContext.PrefixNormalizedDiscordContext.DiscordContext.Channel.SendMessageAsync(
                                echoContext.Content)
                            .ConfigureAwait(false);
                    });
        }
    }
}