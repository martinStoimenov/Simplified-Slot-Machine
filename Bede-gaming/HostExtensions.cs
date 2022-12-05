using Bede_gaming.Services.Interfaces;
using Bede_gaming.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Bede_gaming
{
    public class HostExtensions
    {
        public T GetService<T>()
        {
            using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
                services.AddTransient<IInformationService, InformationService>()
                .AddTransient<IUserInteractionsService, UserInteractionsService>()
                .AddTransient<ISlotMachineConfig, SlotMachineConfig>()
                .AddTransient<ICalculationsService, CalculationsService>())
            .Build();

            return ActivatorUtilities.CreateInstance<T>(host.Services);
        }
    }
}
