using Prism.Ioc;
using Prism.Modularity;

namespace WeatherStation.Modules.Archives
{
    public class ArchivesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Archives>();
        }

    }
}