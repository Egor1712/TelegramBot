using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class StartCommand : ICommand
    {
        private static readonly Regex DataRegex = new Regex(@"(?<Login>.+) : (?<Password>.+)");
        public string Description => "Command /start allows to sing in";

        public async Task Execute(TelegramBotClient client, Message message, User user)
        {
            var answer = null as string;
            await client.SendTextMessageAsync(message.Chat.Id,
                                              "Please send me your login and password in format <login> : <password>");
            while (true)
            {
                var updates = await client.GetUpdatesAsync();
                if (updates.Length == 0)
                {
                    await client.SendTextMessageAsync(message.Chat.Id,
                                                      "Please send me your login and password in format <login> : <password>");
                    continue;
                }
                answer = updates.FirstOrDefault(x => DataRegex.IsMatch(x.Message.Text))?.Message.Text;
                if (string.IsNullOrEmpty(answer))
                    continue;
                break;
            }

            var match = DataRegex.Match(answer);
            var login = match.Groups["Login"].Value;
            var password = match.Groups["Password"].Value;
            await user.Initialize(login, password);
            await client.SendTextMessageAsync(message.Chat.Id, "You successfully login!");
        }
    }
}