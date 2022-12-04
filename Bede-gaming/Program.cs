using Bede_gaming;

namespace BedeGaming
{
    public class Program
    {
        static void Main(string[] args)
        {
            var symbols = SetProbabilityRanges(InitializeSymbols());

            var balance = Welcome();

            Spin(symbols, balance);

            Console.WriteLine();
            Console.WriteLine("End Game...");
        }

        static List<ISymbol> InitializeSymbols()
            => new List<ISymbol>
            {
                new Symbol('A', 0.4m, 45),
                new Symbol('B', 0.6m, 35),
                new Symbol('P', 0.8m, 15),
                new Symbol('*', 0m, 5)
            };

        static List<ISymbol> SetProbabilityRanges(List<ISymbol> symbols)
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

        static decimal Welcome()
        {
            Console.WriteLine("Please deposit money you would like to play with:");
            bool isSuccessfull = decimal.TryParse(Console.ReadLine(), out decimal result);
            var depositAmount = isSuccessfull ? result : 0;

            if (depositAmount <= 0)
            {
                Console.WriteLine("Amount must be number and cannot be less than or equal to zero");
                return Welcome();
            }
            return depositAmount;
        }

        static void Spin(List<ISymbol> symbols, decimal balance)
        {
            while (balance > 0)
            {
                var stake = AskForStake(balance);

                var spinCombinations = new ISymbol[4, 3];
                decimal wonAmountAfterSpin = 0;

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

                wonAmountAfterSpin += GetWonAmountFromCombination(spinCombinations, stake);
                balance = (balance - stake) + wonAmountAfterSpin;

                Console.WriteLine();
                Console.WriteLine("You have won: " + wonAmountAfterSpin.ToString("F"));
                Console.WriteLine("Current balance is: " + balance.ToString("F"));
                Console.WriteLine("-------------------------------------------");
            }
        }

        static decimal GetWonAmountFromCombination(ISymbol[,] spinCombinations, decimal stake)
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

        static decimal AskForStake(decimal balance)
        {
            Console.WriteLine();
            Console.WriteLine("Enter stake amount:");
            bool stakeParse = decimal.TryParse(Console.ReadLine(), out decimal result);
            var stakeAmount = stakeParse ? result : 0;

            if (stakeAmount <= 0)
            {
                Console.WriteLine("Stake must be number and cannot be less than or equal to zero");
                return AskForStake(balance);
            }
            if (stakeAmount > balance)
            {
                Console.WriteLine("Stake cannot be more than your balance");
                return AskForStake(balance);
            }

            Console.WriteLine();
            return stakeAmount;
        }
    }
}