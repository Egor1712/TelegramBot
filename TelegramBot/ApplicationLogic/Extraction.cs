using System;

namespace ApplicationLogic
{
    public class Extraction
    {
        public string DateTime { get; }
        public decimal Payment { get; }
        public decimal Withdrawal { get; }

        public Extraction(string dateTime, decimal payment, decimal withdrawal)
        {
            DateTime = dateTime;
            Payment = payment;
            Withdrawal = withdrawal;
        }
    }
}