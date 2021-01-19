using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class GetSubjectsCommand : ICommand
    {
        public string Description => "Command /subjects gets your subjects and rates";

        public async Task Execute(Message message, User user)
        {
            var subjects = user.Subjects;
            if (subjects.Count == 0)
            {
                await Bot.Bot.SendMessageAsync(message.Chat.Id, "No subjects found =(");
                return;
            }

            var sum = decimal.Zero;
            foreach (var subject in subjects)
            {
                sum += subject.Rate;
                await Bot.Bot.SendMessageAsync(message.Chat.Id, subject.ToString());
            }

            await Bot.Bot.SendMessageAsync(message.Chat.Id,
                                           $"Your average mark is {sum / subjects.Count}");
        }
    }
}