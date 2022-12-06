using Bede_gaming;
using Bede_gaming.Extensions;

namespace BedeGaming
{
    public class Program
    {
        static void Main(string[] args)
        {
            var slotMachine = new HostExtensions().GetService<SlotMachine>();

            slotMachine.Run();
        }
    }
}