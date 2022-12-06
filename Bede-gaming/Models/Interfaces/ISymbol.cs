namespace Bede_gaming.Models.Interfaces
{
    public interface ISymbol
    {
        decimal Coefficient { get; }
        char Name { get; }
        decimal ProbabilityPercentage { get; }
        int ProbabilityRangeEnd { get; set; }
        int ProbabilityRangeStart { get; set; }
    }
}