using Bede_gaming.Extensions;
using Bede_gaming.Services;

namespace SimplifiedSlotMachine.Tests
{
    public class CalculationsServiceTests
    {
        [InlineData(15, 10, 20)]
        [InlineData(100, 50, 0)]
        [InlineData(500, 323, 170)]
        [InlineData(5000000, 4999999, 100000)]
        [Theory]
        public void CalculateBalanceAfterSpinShoudReturnCorrectAmount(decimal balance, decimal stake, decimal wonAmountAfterSpin)
        {
            var result = new HostExtensions().GetService<CalculationsService>().CalculateBalanceAfterSpin(balance, stake, wonAmountAfterSpin);

            var expected = balance - stake + wonAmountAfterSpin;

            Assert.Equal(expected, result);
        }
    }
}