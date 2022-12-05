namespace Bede_gaming.Services.Interfaces
{
    public interface IUserInteractionsService
    {
        decimal AskForStake(decimal balance);
        decimal Welcome();
        void PrintReelCombinations(ref ISymbol[,] spinCombinations, List<ISymbol> symbols);
    }
}