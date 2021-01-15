using System;
using System.Text;

namespace ApplicationLogic
{
    public class CommonRating
    {
        public int Position { get; }
        public int MembersCount { get; }
        public decimal Score { get; }
        public int Year { get; }
        public PeriodType PeriodType { get; }

        public CommonRating(int position, int membersCount, decimal score, int year, PeriodType periodType)
        {
            Position = position;
            MembersCount = membersCount;
            Score = score;
            Year = year;
            PeriodType = periodType;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Вы занимаете {Position} место из {MembersCount}\n");
            builder.Append($"Ваш балл: {Score}");
            builder.Append($"За период {Year}/{GetPeriodTypeOnRussian(PeriodType)}");
            return builder.ToString();
        }

        private static string GetPeriodTypeOnRussian(PeriodType type)
        {
            return type == PeriodType.Term ? "семестр" : "год";
        }
    }
}