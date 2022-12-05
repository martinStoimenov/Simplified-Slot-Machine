using Bede_gaming.Services.Interfaces;

namespace Bede_gaming.Services
{
    public interface ICalculationsService
    {
        decimal GetWonAmountFromCombination(ISymbol[,] spinCombinations, decimal stake);
        decimal CalculateBalanceAfterSpin(decimal balance, decimal stake, decimal wonAmountAfterSpin);
    }
}