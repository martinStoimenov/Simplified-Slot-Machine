namespace Bede_gaming.Services.Interfaces
{
    public interface ISlotMachineConfig
    {
        IEnumerable<ISymbol> GetSymbolsFromFile();
    }
}