using System.Collections.Generic;

namespace ApplicationLogic
{
    public class DormitoryService
    {
        public decimal Debt { get; }
        private readonly List<Extraction> extractions = new List<Extraction>();
        public IReadOnlyCollection<Extraction> Extractions => extractions;

        public DormitoryService(decimal debt)
        {
            Debt = debt;
        }

        public void AddExtraction(Extraction extraction)
        {
            extractions.Add(extraction);
        }
    }
}