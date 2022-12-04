namespace Bede_gaming.Services.Interfaces
{
    public interface IInformationService
    {
        void WriteLine(string input);

        void Write(string input);

        void DisplayResultAfterSpin(decimal wonAmountAfterSpin, decimal balance);
    }
}