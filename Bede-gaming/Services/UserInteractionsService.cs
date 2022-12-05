using Bede_gaming.Services.Interfaces;

namespace Bede_gaming.Services
{
    public class UserInteractionsService : IUserInteractionsService
    {
        private readonly IInformationService _infoService;
        
        public UserInteractionsService(IInformationService infoService)
        {
            _infoService = infoService;
        }

        public decimal Welcome()
        {
            _infoService.WriteLine("Please deposit money you would like to play with:");
            bool isSuccessfull = decimal.TryParse(Console.ReadLine(), out decimal result);
            var depositAmount = isSuccessfull ? result : 0;

            if (depositAmount <= 0)
            {
                _infoService.WriteLine("Amount must be number and cannot be less than or equal to zero");
                return Welcome();
            }
            return depositAmount;
        }

        public decimal AskForStake(decimal balance)
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
        public void PrintReelCombinations(ref ISymbol[,] spinCombinations, List<ISymbol> symbols)
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
        }
    }
}
