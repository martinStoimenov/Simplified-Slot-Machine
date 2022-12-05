using Bede_gaming.Services;
using Bede_gaming.Services.Interfaces;

namespace Bede_gaming
{
    public class SlotMachine
    {
        private readonly IInformationService infoService;
        private readonly IUserInteractionsService userInteractionService;
        private readonly ISlotMachineConfig slotMachineConfig;
        private readonly ICalculationsService calcService;

        public SlotMachine(IInformationService infoService,
            IUserInteractionsService userInteractionService,
            ISlotMachineConfig slotMachineConfig,
            ICalculationsService calcService)
        {
            this.infoService = infoService;
            this.userInteractionService = userInteractionService;
            this.slotMachineConfig = slotMachineConfig;
            this.calcService = calcService;
        }

        public void Run()
        {
            var symbols = slotMachineConfig.GetSymbolsFromFile();

            var balance = userInteractionService.Welcome();

            SpinReel(symbols, balance);

            infoService.WriteLine("\nEnd Game...");
        }

        private void SpinReel(IEnumerable<ISymbol> symbols, decimal balance)
        {
            while (balance > 0)
            {
                var stake = userInteractionService.AskForStake(balance);

                var spinCombinations = new ISymbol[4, 3];

                userInteractionService.PrintReelCombinations(ref spinCombinations, symbols.ToList());

                var wonAmountAfterSpin = calcService.GetWonAmountFromCombination(spinCombinations, stake);

                balance = calcService.CalculateBalanceAfterSpin(balance, stake, wonAmountAfterSpin);

                infoService.DisplayResultAfterSpin(wonAmountAfterSpin, balance);
            }
        }
    }
}
