using Bede_gaming.Models;
using Bede_gaming.Models.Interfaces;

namespace Bede_gaming.Services
{
    public class CalculationsService : ICalculationsService
    {
        public void SetProbabilityRanges(ref List<Symbol> symbols)
        {
            for (int i = 0; i < symbols.Count(); i++)
            {
                if (i == 0)
                {
                    symbols[i].ProbabilityRangeStart = 1;
                    symbols[i].ProbabilityRangeEnd = decimal.ToInt32(symbols[i].ProbabilityPercentage);
                }
                else
                {
                    symbols[i].ProbabilityRangeStart = symbols[i - 1].ProbabilityRangeEnd + 1;
                    symbols[i].ProbabilityRangeEnd = decimal.ToInt32(symbols[i - 1].ProbabilityRangeEnd) + decimal.ToInt32(symbols[i].ProbabilityPercentage);
                }
            }
        }
        public decimal GetWonAmountFromCombination(ISymbol[,] spinCombinations, decimal stake)
        {
            decimal amount = 0;

            for (int i = 0; i < spinCombinations.GetLength(0); i++)
            {
                var currentRow = Enumerable.Range(0, spinCombinations.GetLength(1)).Select(x => spinCombinations[i, x]).ToArray();
                //AAA , BBB
                if (currentRow.Distinct().Count() == 1)
                    amount += (currentRow[0].Coefficient * 3) * stake;
                //*P*, **A, *AB
                else if (currentRow.Select(x => x.Name).Contains('*'))
                {
                    // *AA, *PP
                    if (currentRow.Where(x => x.Name != '*').Distinct().Count() == 1)
                        amount += (currentRow.Where(x => x.Name != '*').First().Coefficient * 2) * stake;

                    // **B
                    if (currentRow.Where(x => x.Name != '*').Count() == 1)
                        amount += currentRow.Where(x => x.Name != '*').First().Coefficient * stake;
                }
            }
            return amount;
        }

        public decimal CalculateBalanceAfterSpin(decimal balance, decimal stake, decimal wonAmountAfterSpin)
        => balance - stake + wonAmountAfterSpin;
    }
}
