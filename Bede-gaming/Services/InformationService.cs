using Bede_gaming.Services.Interfaces;

namespace Bede_gaming.Services
{
    public class InformationService : IInformationService
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

        public void Write(string input)
        {
            Console.Write(input);
        }

        public void DisplayResultAfterSpin(decimal wonAmountAfterSpin, decimal balance)
        {
            Console.WriteLine();
            Console.WriteLine("You have won: " + wonAmountAfterSpin.ToString("F"));
            Console.WriteLine("Current balance is: " + balance.ToString("F"));
            Console.WriteLine("-------------------------------------------");
        }
    }
}
