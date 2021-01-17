using System;
using System.Text.RegularExpressions;

namespace Services.Commands
{
    public static class CommandParser
    {
        private static readonly Regex DebtRegex =
            new Regex("/debt", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PaymentsRegex =
            new Regex("/payments", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex SubjectsRegex =
            new Regex("/subjects", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex StartCommandRegex = new Regex("/start");
        private static readonly  Regex HelpCommandRegex = new Regex(@"/help");

        public static ICommand GetCommand(string text)
        {
            if (DebtRegex.IsMatch(text))
                return new GetDebtCommand();
            if (PaymentsRegex.IsMatch(text))
                return new GetPaymentsCommand();
            if (SubjectsRegex.IsMatch(text))
                return new GetSubjectsCommand();
            if (StartCommandRegex.IsMatch(text))
                return new StartCommand();
            if (HelpCommandRegex.IsMatch(text))
                return new HelpCommand();
            // TODO need refactor on DI container
            throw new Exception($"Any command is match text {text}");
        }
    }
}