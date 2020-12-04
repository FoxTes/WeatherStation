using Prism.Ioc;
using Prism.Modularity;
using Serilog;
using System.Windows;
using WeatherStation.Modules.Archives;
using WeatherStation.Modules.ConnectionDevice;
using WeatherStation.Modules.NotificationViewer;
using WeatherStation.Modules.RealtimeDataViewer;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Services.Notification;
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
            containerRegistry.RegisterSingleton<ICommunicationService, CommunicationService>();
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<RealtimeDataViewerModule>();
            moduleCatalog.AddModule<ConnectionDeviceModule>();
            moduleCatalog.AddModule<NotificationViewerModule>();
            moduleCatalog.AddModule<ArchivesModule>();
        }        
        
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
