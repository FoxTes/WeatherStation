using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeatherStation.Core;

namespace WeatherStation.Modules.RealtimeDataViewer
{
    public class RealtimeDataViewerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public RealtimeDataViewerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.MainContent, "RealtimeDataViewer");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.RealtimeDataViewer>();
        }
    }
}