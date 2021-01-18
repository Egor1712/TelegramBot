using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetPaymentsCommand : ICommand
    {
        public string Description => "Command /payments gets your dormitory payments";
        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            if (user.DormitoryService.Extractions.Count == 0)
            {
                await bot.SendMessageAsync(message.Chat.Id, " You haven't any payments =(");
                return;
            }
            
            foreach (var extraction in user.DormitoryService.Extractions)
            {
                await bot.SendMessageAsync(message.Chat.Id, extraction.ToString());
            }
        }
    }
}