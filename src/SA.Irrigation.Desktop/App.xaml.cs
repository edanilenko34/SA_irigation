using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SA.Irrigation.Desktop.RestApiClient;
using System.Windows;

namespace SA.Irrigation.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration config { get; private set; }

        public static IHost? AppHost { get; private set; }

        public App()
        {

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    

                    services.AddRefitClient<ISAIrrigationAPI>()
                        .ConfigureHttpClient(c=> c.BaseAddress = new Uri(config.GetConnectionString("serviceUrl")));

                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
