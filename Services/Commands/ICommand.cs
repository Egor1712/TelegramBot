using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public interface ICommand
    { 
        string Description { get; }
        Task Execute(Message message, User user);
    }
}