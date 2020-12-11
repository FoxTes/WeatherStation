using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using WeatherStation.BusinessAccess.Sqlite;
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
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICommunicationService, CommunicationService>();
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();
            containerRegistry.RegisterSingleton<ISqliteService, SqliteService>();
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
