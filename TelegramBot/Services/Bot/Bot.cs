using System;
using System.Threading.Tasks;
using Services.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Services.Bot
{
    public class Bot
    {
        private readonly TelegramBotClient botClient = new TelegramBotClient(BotInfo.BotToken);
        private readonly User  user = new User();

        public Bot()
        {
            botClient.OnMessage += BotClientOnOnMessage;
            botClient.StartReceiving();
        }
        
        private async void BotClientOnOnMessage(object? sender, MessageEventArgs e)
        {
            try
            {
                var command = CommandParser.GetCommand(e.Message.Text);
                botClient.StopReceiving();
                await command.Execute(botClient, e.Message, user);
                botClient.StartReceiving();
            }
            catch (Exception exception)
            {
                if (e.Message.Text.StartsWith("/"))
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id,
                                                     $"Unknown command {e.Message.Text}! Please use /help");
            }
        }
    }
}