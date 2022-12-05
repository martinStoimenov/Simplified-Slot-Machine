namespace Bede_gaming.Models
{
    public abstract class SymbolBase
    {
        public char Name { get; private set; }
        public decimal Coefficient { get; private set; }
        public decimal ProbabilityPercentage { get; private set; }

        public SymbolBase(char name, decimal coefficient, decimal probabilityPercentage)
        {
            Name = name;
            Coefficient = coefficient;
            ProbabilityPercentage = probabilityPercentage;
        }
    }
}
