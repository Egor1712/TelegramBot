using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public interface ICommand
    { 
        string Description { get; }
        Task Execute(TelegramBotClient client, Message message, User user);
    }
}