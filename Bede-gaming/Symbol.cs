namespace Bede_gaming
{
    public class Symbol : ISymbol
    {
        public char Name { get; private set; }
        public decimal Coefficient { get; private set; }
        public decimal ProbabilityPercentage { get; private set; }
        public int ProbabilityRangeStart { get; set; }
        public int ProbabilityRangeEnd { get; set; }

        public Symbol(char name, decimal coefficient, decimal probabilityPercentage)
        {
            Name = name;
            Coefficient = coefficient;
            ProbabilityPercentage = probabilityPercentage;
            ProbabilityRangeStart = 0;
            ProbabilityRangeEnd = 0;
        }
    }
}
