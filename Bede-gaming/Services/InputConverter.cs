using Bede_gaming.Services.Interfaces;

namespace Bede_gaming.Services
{
    public class InputConverter : IInputConverter
    {
        public decimal TryConvertToDecimal(string? input)
        {
            bool isSuccessfull = decimal.TryParse(input, out decimal result);
            return isSuccessfull ? result : 0;
        }
    }
}
