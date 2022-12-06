namespace Bede_gaming.Services.Interfaces
{
    public interface IInputConverter
    {
        decimal TryConvertToDecimal(string? input);
    }
}