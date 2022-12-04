using Bede_gaming;
using Bede_gaming.Services;

namespace BedeGaming
{
    public class Program
    {
        static void Main(string[] args)
        {
            //TODO try to create DI container and use it
            var infoService = new InformationService();
            var userInteractionService = new UserInteractionsService(infoService);

            var slotMachine = new SlotMachine(infoService, userInteractionService);
            slotMachine.Run();
        }
    }
}