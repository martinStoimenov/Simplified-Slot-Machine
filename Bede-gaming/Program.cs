using Bede_gaming;

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