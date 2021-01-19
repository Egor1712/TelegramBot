#nullable enable
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Services.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Bot
{
    public class Bot
    {
        private static readonly Regex DataRegex = new Regex(@"(?<Login>.+) : (?<Password>.+)");
        private static readonly TelegramBotClient BotClient = new TelegramBotClient(BotInfo.BotToken);
        private static readonly Dictionary<long, User> Users = new Dictionary<long, User>();
        
        public static async Task SendMessageAsync(long chatId, string message)
        {
            await BotClient.SendTextMessageAsync(chatId, message);
        }

        private static async Task<bool> TryAuthorizeAsync(long chatId, string text)
        {
            var user = Users[chatId];
            if (!DataRegex.IsMatch(text))
            {
                await SendMessageAsync(chatId,
                                       "Please send me your login and password in format <login> : <password>");
                return false;
            }

            var match = DataRegex.Match(text);
            var login = match.Groups["Login"].Value;
            var password = match.Groups["Password"].Value;
            await user.Initialize(login, password);
            await BotClient.SendTextMessageAsync(chatId, "You successfully login!");
            return true;
        }


        public static async Task BotClientOnOnMessage(Message message)
        {
            try
            {
                var chatId = message.Chat.Id;
                if (!Users.ContainsKey(chatId))
                {
                    Users[chatId] = new User();
                    await SendMessageAsync(chatId,
                                           "Please send me your login and password in format <login> : <password>");
                    return;
                }

                var user = Users[chatId];
                if (!user.IsAuthorize)
                {
                    if (!await TryAuthorizeAsync(chatId, message.Text))
                        await SendMessageAsync(chatId,
                                               "Please send me your login and password in format <login> : <password>");
                    return;
                }

                var command = CommandParser.GetCommand(message.Text);
                await command.Execute( message, user);
            }
            catch (Exception exception)
            {
                if (message.Text.StartsWith("/"))
                    await BotClient.SendTextMessageAsync(message.Chat.Id,
                                                         $"Unknown command {message.Text}! Please use /help");
                Console.WriteLine(exception.Message);
                Console.WriteLine(message.Text);
                Console.WriteLine(exception.StackTrace);
            }
        }

        public static void UnloginUserByChatId(long chatId)
        {
            if (Users.ContainsKey(chatId))
                Users.Remove(chatId);
            Users[chatId] = new User();
        }

        public static void SetWebHook()
        {
            BotClient.SetWebhookAsync($"{BotInfo.Url}update").Wait();
        }
    }
}