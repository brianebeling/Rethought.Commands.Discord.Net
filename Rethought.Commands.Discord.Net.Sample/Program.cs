using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Optional;
using Rethought.Commands.Builder;
using Rethought.Commands.Discord.Net.Prefix;
using Rethought.Commands.Discord.Net.TypeParsers;
using Rethought.Commands.Parser;

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

            await discordSocketClient.LoginAsync(TokenType.Bot, "TOKEN");
            await discordSocketClient.StartAsync();

            var discordContextFactory = new DiscordContextFactory(discordSocketClient);

            var asyncFuncBuilder = AsyncFuncBuilder<DiscordContext>.Create();

            discordSocketClient.Ready += () =>
            {
                var typeParserDictionary = new Dictionary<Type, ITypeParser<string, object>>()
                {
                    {typeof(uint), new ObjectTypeParserWrapper<string, uint>(Commands.Parser.Func<string, uint>.Create(s => Option.Some(uint.Parse(s))))}
                };

                asyncFuncBuilder
                    .WithCondition(context => !context.User.IsBot)
                    .WithCondition(context => !context.IsPrivateChannel)
                    .WithPrefix(
                        new MentionPrefix(discordSocketClient.CurrentUser),
                        root =>
                            root.WithGroup(
                                new StringPrefix("misc"),
                                new List<Action<AsyncFuncBuilder<NormalizedDiscordContext>>>
                                {
                                    echo => echo.WithPrefix(
                                        new StringPrefix("echo"),
                                        command => command.WithAdapter(
                                            Commands.Parser.Auto.Builder.Create()
                                                .WithTypeParser(parameter => Option.Some(uint.Parse(parameter)))
                                                .Build<NormalizedDiscordContext, EchoContext>(input => input.NormalizedMessage),
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
                                            "Sorry, I don't know that command!");
                                    })));

                var asyncAction = asyncFuncBuilder.Build();

                discordSocketClient.MessageReceived += async message =>
                {
                    if (message is IUserMessage userMessage)
                    {
                        await asyncAction.InvokeAsync(
                            discordContextFactory.Create(userMessage),
                            CancellationToken.None);
                    }
                };

                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private static Action<AsyncFuncBuilder<ReverseContext>> CreateReverseConfiguration()
        {
            return reverse => reverse
                .WithAsyncFunc(
                    async reverseContext =>
                    {
                        await reverseContext.DiscordContext.Channel.SendMessageAsync(
                            new string(reverseContext.NormalizedMessage.Reverse().ToArray()));
                    });
        }


        private static Action<AsyncFuncBuilder<EchoContext>> CreateEchoConfiguration()
        {
            return echo => echo
                .WithAsyncFunc(
                    async echoContext =>
                    {
                        await echoContext.DiscordContext.Channel.SendMessageAsync(
                            echoContext.NormalizedMessage);
                    });
        }
    }
}