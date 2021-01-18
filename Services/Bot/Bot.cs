using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Services.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Services.Bot
{
    public class Bot
    {
        private static readonly Regex DataRegex = new Regex(@"(?<Login>.+) : (?<Password>.+)");
        private readonly TelegramBotClient botClient = new TelegramBotClient(BotInfo.BotToken);
        private readonly Dictionary<long, User> users = new Dictionary<long, User>();

        public Bot()
        {
            botClient.DeleteWebhookAsync().Wait();
            botClient.OnMessage += BotClientOnOnMessage;
            botClient.StartReceiving();
        }

        private async void BotClientOnOnMessage(object? sender, MessageEventArgs eventArgs)
        {
            var message = eventArgs.Message;
            try
            {
                var chatId = message.Chat.Id;
                if (!users.ContainsKey(chatId))
                {
                    users[chatId] = new User();
                    await botClient.SendTextMessageAsync(chatId,
                                                         "Please send me your login and password in format <login> : <password>");
                    return;
                }

                var user = users[chatId];
                if (!user.IsAuthorize)
                {
                    if (!DataRegex.IsMatch(message.Text))
                    {
                        await botClient.SendTextMessageAsync(chatId,
                                                             "Please send me your login and password in format <login> : <password>");
                        return;
                    }

                    var match = DataRegex.Match(message.Text);
                    var login = match.Groups["Login"].Value;
                    var password = match.Groups["Password"].Value;
                    await user.Initialize(login, password);
                    await botClient.SendTextMessageAsync(chatId, "You successfully login!");
                    return;
                }

                var command = CommandParser.GetCommand(message.Text);
                await command.Execute(botClient, message, user);
            }
            catch (Exception exception)
            {
                if (message.Text.StartsWith("/"))
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                                                         $"Unknown command {message.Text}! Please use /help");
                Console.WriteLine(exception.Message);
                Console.WriteLine(message.Text);
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}