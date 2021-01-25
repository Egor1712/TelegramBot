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
    public static class Bot
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
                return false;
            }

            var match = DataRegex.Match(text);
            var login = match.Groups["Login"].Value;
            var password = match.Groups["Password"].Value;
            return await user.Initialize(login, password);
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
                    else
                        await BotClient.SendTextMessageAsync(chatId, "You successfully login!");
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
            }
        }

        public static void UnloginUserByChatId(long chatId)
        {
            if (Users.ContainsKey(chatId))
                Users.Remove(chatId);
            Users[chatId] = new User();
        }

        public static async Task SetWebHook()
        {
            await BotClient.DeleteWebhookAsync();
            await BotClient.SetWebhookAsync(BotInfo.Url);
        }
    }
}