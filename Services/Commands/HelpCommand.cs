﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Services.Commands
{
    public class HelpCommand : ICommand
    {
        private static readonly List<ICommand> Commands = new List<ICommand>
                                                          {
                                                              new HelpCommand(),
                                                              new StartCommand(),
                                                              new GetDebtCommand(),
                                                              new GetPaymentsCommand(),
                                                              new GetSubjectsCommand(),
                                                              new UnloginCommand()
                                                          };

        public string Description => "Command /help gets all available commands";

        public async Task Execute(Bot.Bot bot, Message message, User user)
        {
            var builder = new StringBuilder();
            foreach (var command in Commands)
                builder.Append($"{command.Description}\n");

            await bot.SendMessageAsync(message.Chat.Id, builder.ToString());
        }
    }
}