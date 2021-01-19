namespace ApplicationLogic
{
    public class Subject
    {
        public string Name { get; }
        public decimal Rate { get; }

        public Subject(string name, decimal rate)
        {
            this.Name = name;
            this.Rate = rate;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Subject subject))
                return false;
            return Name.Equals(subject.Name);
        }

        public override string ToString()
        {
            return $"{Name} : {Rate}";
        }

        public bool IsSameRate(Subject subject)
        {
            return Rate.Equals(subject.Rate);
        }
    }
}