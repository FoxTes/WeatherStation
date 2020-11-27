using Prism.Ioc;
using Prism.Modularity;
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
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICommunicationService, CommunicationService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ConnectionDeviceModule>();
            moduleCatalog.AddModule<RealtimeDataViewerModule>();
            moduleCatalog.AddModule<ArchivesModule>();
        }
    }
}
