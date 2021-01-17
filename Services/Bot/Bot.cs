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
            botClient.OnMessage += BotClientOnOnMessage;
            botClient.StartReceiving();
        }

        private async void BotClientOnOnMessage(object? sender, MessageEventArgs e)
        {
            try
            {
                var chatId = e.Message.Chat.Id;
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
                    if (!DataRegex.IsMatch(e.Message.Text))
                    {
                        await botClient.SendTextMessageAsync(chatId,
                                                             "Please send me your login and password in format <login> : <password>");
                        return;
                    }

                    var match = DataRegex.Match(e.Message.Text);
                    var login = match.Groups["Login"].Value;
                    var password = match.Groups["Password"].Value;
                    await user.Initialize(login, password);
                    await botClient.SendTextMessageAsync(chatId, "You successfully login!");
                    return;
                }

                var command = CommandParser.GetCommand(e.Message.Text);
                await command.Execute(botClient, e.Message, user);
            }
            catch (Exception exception)
            {
                if (e.Message.Text.StartsWith("/"))
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id,
                                                         $"Unknown command {e.Message.Text}! Please use /help");
                Console.WriteLine(exception.Message);
                Console.WriteLine(e.Message.Text);
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}