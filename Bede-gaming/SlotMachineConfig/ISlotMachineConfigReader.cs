using Bede_gaming.Models.Interfaces;

namespace Bede_gaming.SlotMachineConfig
{
    public interface ISlotMachineConfigReader
    {
        IEnumerable<ISymbol> GetSymbolsFromFile();
    }
}