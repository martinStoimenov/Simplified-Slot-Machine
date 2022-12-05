using Bede_gaming.Services.Interfaces;

namespace Bede_gaming.Models
{
    public class Symbol : SymbolBase, ISymbol
    {
        public int ProbabilityRangeStart { get; set; }
        public int ProbabilityRangeEnd { get; set; }

        public Symbol(char name, decimal coefficient, decimal probabilityPercentage) 
            : base(name, coefficient, probabilityPercentage)
        {
            ProbabilityRangeStart = 0;
            ProbabilityRangeEnd = 0;
        }
    }
}
