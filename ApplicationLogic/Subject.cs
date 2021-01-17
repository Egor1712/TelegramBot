namespace ApplicationLogic
{
    public class Subject
    {
        private readonly string name;
        private readonly decimal rate;

        public Subject(string name, decimal rate)
        {
            this.name = name;
            this.rate = rate;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Subject subject))
                return false;
            return name.Equals(subject.name);
        }

        public override string ToString()
        {
            return $"{name} : {rate}";
        }

        public bool IsSameRate(Subject subject)
        {
            return rate.Equals(subject.rate);
        }
    }
}