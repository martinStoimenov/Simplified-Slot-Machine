using Bede_gaming.Models;
using Bede_gaming.Services.Interfaces;
using System.Text.Json;

namespace Bede_gaming.Services
{
    public class SlotMachineConfig : ISlotMachineConfig
    {
        public IEnumerable<ISymbol> GetSymbolsFromFile()
        {
            var symbols =  this.ReadAsync<List<Symbol>>(@"..\..\..\gameConfig.json");

            this.SetProbabilityRanges(ref symbols);
            return symbols;
        }

        private T ReadAsync<T>(string filePath)
        {
            using var stream = new StreamReader(filePath);
            var jsonStr = stream.ReadToEnd();
            var a = JsonSerializer.Deserialize<T>(jsonStr);
            return a;
        }

        private void SetProbabilityRanges(ref List<Symbol> symbols)
        {
            for (int i = 0; i < symbols.Count(); i++)
            {
                if (i == 0)
                {
                    symbols[i].ProbabilityRangeStart = 1;
                    symbols[i].ProbabilityRangeEnd = decimal.ToInt32(symbols[i].ProbabilityPercentage);
                }
                else
                {
                    symbols[i].ProbabilityRangeStart = symbols[i - 1].ProbabilityRangeEnd + 1;
                    symbols[i].ProbabilityRangeEnd = decimal.ToInt32(symbols[i - 1].ProbabilityRangeEnd) + decimal.ToInt32(symbols[i].ProbabilityPercentage);
                }
            }
        }
    }
}
