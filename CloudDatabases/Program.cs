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

                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IMortgageService, MortgageService>();
                    services.AddTransient<IHouseService, HouseService>();
                    services.AddTransient<IUserRepository, UserRepository>();
                    services.AddTransient<IHouseRepository, HouseRepository>();

                    services.AddDbContext<UserContext>();
                    services.AddDbContext<MortgageContext>();
                    services.AddDbContext<HouseContext>();

                    services.AddSingleton<IBlob, Blob>();
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}