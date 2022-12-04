namespace Bede_gaming
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