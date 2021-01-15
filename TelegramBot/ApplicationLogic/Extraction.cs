using System;

namespace ApplicationLogic
{
    public class Extraction
    {
        public DateTime DateTime { get; }
        public decimal Payment { get; }
        public decimal Withdrawal { get; }

        public Extraction(DateTime dateTime, decimal payment, decimal withdrawal)
        {
            DateTime = dateTime;
            Payment = payment;
            Withdrawal = withdrawal;
        }
    }
}