using Bede_gaming.Models;
using Bede_gaming.Services.Interfaces;

namespace Bede_gaming
{
    public class SlotMachine
    {
        private readonly IInformationService _infoService;
        private readonly IUserInteractionsService _userInteractionService;

        public SlotMachine(IInformationService infoService, IUserInteractionsService userInteractionService)
        {
            _infoService = infoService;
            _userInteractionService = userInteractionService;
        }

        public void Run()
        {
            var symbols = SetProbabilityRanges(InitializeSymbols());

            var balance = _userInteractionService.Welcome();

            Spin(symbols, balance);

            _infoService.WriteLine("\nEnd Game...");
        }

        public void Spin(List<ISymbol> symbols, decimal balance)
        {
            while (balance > 0)
            {
                var stake = _userInteractionService.AskForStake(balance);

                var spinCombinations = new ISymbol[4, 3];
                decimal wonAmountAfterSpin = 0;

                spinCombinations = GetProbabilityRanges(spinCombinations, symbols);

                wonAmountAfterSpin += GetWonAmountFromCombination(spinCombinations, stake);
                balance = (balance - stake) + wonAmountAfterSpin;

                _infoService.DisplayResultAfterSpin(wonAmountAfterSpin, balance);
            }
        }

        private ISymbol[,] GetProbabilityRanges(ISymbol[,] spinCombinations, List<ISymbol> symbols)
        {
            for (int i = 0; i < spinCombinations.GetLength(0); i++)
            {
                for (int j = 0; j < spinCombinations.GetLength(1); j++)
                {
                    Random random = new Random();
                    int randomRangeEnd = symbols[symbols.IndexOf(symbols.Last())].ProbabilityRangeEnd + 1;
                    int randomNumber = random.Next(symbols[0].ProbabilityRangeStart, randomRangeEnd);

                    var symbol = symbols.Where(x => randomNumber >= x.ProbabilityRangeStart && randomNumber <= x.ProbabilityRangeEnd).First();

                    spinCombinations[i, j] = symbol;
                    Console.Write(symbol.Name);
                }
                Console.WriteLine();
            }
            //TODO think of error conditions OR return default value which will not break the program
            return spinCombinations;
        }

        private List<ISymbol> SetProbabilityRanges(List<ISymbol> symbols)
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
            return symbols;
        }

        private decimal GetWonAmountFromCombination(ISymbol[,] spinCombinations, decimal stake)
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

        private List<ISymbol> InitializeSymbols()
            => new List<ISymbol>
            {
                new Symbol('A', 0.4m, 45),
                new Symbol('B', 0.6m, 35),
                new Symbol('P', 0.8m, 15),
                new Symbol('*', 0m, 5)
            };        
    }
}
