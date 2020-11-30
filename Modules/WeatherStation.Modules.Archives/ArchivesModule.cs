using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeatherStation.Core;

namespace WeatherStation.Modules.Archives
{
    public class ArchivesModule : IModule
    {
        //private readonly IRegionManager _regionManager;

        public ArchivesModule(IRegionManager regionManager)
        {
            //_regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.MainContent, "Archives");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Archives>();
        }
    }
}