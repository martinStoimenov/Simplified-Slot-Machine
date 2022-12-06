using Bede_gaming.Models;
using Bede_gaming.Models.Interfaces;
using Bede_gaming.Services;
using System.Text.Json;

namespace Bede_gaming.SlotMachineConfig
{
    public class SlotMachineConfigReader : ISlotMachineConfigReader
    {
        private readonly ICalculationsService calculationsService;
        public SlotMachineConfigReader(ICalculationsService calculationsService)
        {
            this.calculationsService = calculationsService;
        }

        public IEnumerable<ISymbol> GetSymbolsFromFile()
        {
            var symbols =  this.ReadAsync<List<Symbol>>(@"..\..\..\SlotMachineConfig\gameConfig.json");

            calculationsService.SetProbabilityRanges(ref symbols);
            return symbols;
        }

        private T ReadAsync<T>(string filePath)
        {
            using var stream = new StreamReader(filePath);
            var jsonStr = stream.ReadToEnd();
            var a = JsonSerializer.Deserialize<T>(jsonStr);
            return a;
        }
    }
}
