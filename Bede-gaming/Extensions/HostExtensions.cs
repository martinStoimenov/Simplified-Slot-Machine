using Bede_gaming.Services.Interfaces;
using Bede_gaming.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bede_gaming.SlotMachineConfig;

namespace Bede_gaming.Extensions
{
    public class HostExtensions
    {
        public T GetService<T>()
        {
            using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
                services.AddTransient<IInformationService, InformationService>()
                .AddTransient<IInputConverter, InputConverter>()
                .AddTransient<IUserInteractionsService, UserInteractionsService>()
                .AddTransient<ISlotMachineConfigReader, SlotMachineConfigReader>()
                .AddTransient<ICalculationsService, CalculationsService>())
            .Build();

            return ActivatorUtilities.CreateInstance<T>(host.Services);
        }
    }
}
