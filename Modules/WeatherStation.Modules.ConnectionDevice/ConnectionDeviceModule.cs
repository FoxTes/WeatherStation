using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeatherStation.Core;

namespace WeatherStation.Modules.ConnectionDevice
{
    public class ConnectionDeviceModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ConnectionDeviceModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ConnectionDevice");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.ConnectionDevice>();
        }
    }
}