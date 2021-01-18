using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public interface ICommand
    { 
        string Description { get; }
        Task Execute(Bot.Bot bot, Message message, User user);
    }
}