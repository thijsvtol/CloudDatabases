using DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;

namespace CloudDatabases
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    // services
                    services.AddTransient<IHouseService, HouseService>();

                    // repos
                    services.AddTransient<IHouseRepository, HouseRepository>();

                    services.AddDbContext<HouseContext>();

                    // cosmosdb setup
                    // services.AddSingleton<ICosmosDbService<House>>(CosmosDbSetup<House>
                    //     .InitializeCosmosClientInstanceAsync("CourseContainer", "/id")
                    //     .GetAwaiter().GetResult());
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}