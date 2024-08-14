using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MauiAppForm.Services;
namespace MauiAppForm
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }


        public App()
        {
            InitializeComponent();
            // Initialize the service provider
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
     

            MainPage = new AppShell();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            // Register your services here
            services.AddSingleton<IPrintService, PrintService>();
        }

    }
}
