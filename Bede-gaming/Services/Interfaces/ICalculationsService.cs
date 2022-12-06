using Bede_gaming.Models;
using Bede_gaming.Models.Interfaces;

namespace Bede_gaming.Services
{
    public interface ICalculationsService
    {
        decimal GetWonAmountFromCombination(ISymbol[,] spinCombinations, decimal stake);
        decimal CalculateBalanceAfterSpin(decimal balance, decimal stake, decimal wonAmountAfterSpin);
        void SetProbabilityRanges(ref List<Symbol> symbols);
    }
}