using Prism.Ioc;
using Prism.Modularity;
using Serilog;
using System.Windows;
using WeatherStation.Modules.Archives;
using WeatherStation.Modules.ConnectionDevice;
using WeatherStation.Modules.RealtimeDataViewer;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Views;

namespace WeatherStation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: "logger/MyApp.log")
                .CreateLogger();
            Log.Logger.Information("Start application!");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Close application!");
            Log.CloseAndFlush();

            base.OnExit(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSerilog();
            containerRegistry.RegisterSingleton<ICommunicationService, CommunicationService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ConnectionDeviceModule>();
            moduleCatalog.AddModule<RealtimeDataViewerModule>();
            moduleCatalog.AddModule<ArchivesModule>();
        }        
        
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
