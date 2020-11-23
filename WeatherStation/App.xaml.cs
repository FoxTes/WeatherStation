using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using WeatherStation.Modules.ModuleName;
using WeatherStation.Services;
using WeatherStation.Services.Interfaces;
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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
