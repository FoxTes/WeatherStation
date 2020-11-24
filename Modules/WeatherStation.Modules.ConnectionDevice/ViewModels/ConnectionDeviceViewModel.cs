using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Core.Mvvm;
using WeatherStation.Services.CommunicationService;

namespace WeatherStation.Modules.ConnectionDevice.ViewModels
{
    public class ConnectionDeviceViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ConnectionDeviceViewModel(IRegionManager regionManager, ICommunicationService communicationService) : base(regionManager)
        {
            Task.Run(() =>
            {
                var namePort = communicationService.GetNamePorts();

                foreach (var item in namePort)
                {

                    bool isFault = false;
                    try
                    {
                        communicationService.Open(item);
                    }
                    catch (Exception)
                    {
                        isFault = true;
                    }
                    finally
                    {
                        if (!isFault)
                        {
                            var data = new byte[3] { 0x05, 0x05, 0x05 };
                            communicationService.SendData(data);
                        }
                        Thread.Sleep(3000);
                    }
                }
            });
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
