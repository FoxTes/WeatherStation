using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeatherStation.Core;

namespace WeatherStation.Modules.NotificationViewer
{
    public class NotificationViewerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NotificationViewerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.Notification, "NotificationViewer");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.NotificationViewer>();
        }
    }
}