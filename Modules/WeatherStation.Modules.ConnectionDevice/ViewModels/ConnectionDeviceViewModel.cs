using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WeatherStation.Core;
using WeatherStation.Core.Mvvm;
using WeatherStation.Services.CommunicationService;

namespace WeatherStation.Modules.ConnectionDevice.ViewModels
{
    public class ConnectionDeviceViewModel : RegionViewModelBase
    {
        #region Filed
        private readonly IEventAggregator _eventAggregator;
        private readonly ICommunicationService _communicationService;

        private string _nameConnectionDevice = "NameConnectionDevice";
        private string _connectionStatus = "Не подключено";
        private string _countReciveParcel = null;
        #endregion

        #region Property
        public string NameConnectionDevice
        {
            get { return _nameConnectionDevice; }
            set { SetProperty(ref _nameConnectionDevice, value); }
        }

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set { SetProperty(ref _connectionStatus, value); }
        }

        public string CountReciveParcel
        {
            get { return _countReciveParcel; }
            set { SetProperty(ref _countReciveParcel, value); }
        }
        #endregion

        #region Constructor
        public ConnectionDeviceViewModel(IRegionManager regionManager, ICommunicationService communicationService, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _communicationService = communicationService;

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);

            //Task.Run(() =>
            //{
            //    var result = _communicationService.ConnectDeviceAsync().Result;
            //    if (result != null)
            //    {
            //        NameConnectionDevice = System.Text.Encoding.Default.GetString(result);
            //    }
            //});
        }

        private void MessageReceived(bool command)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = new Task(() =>
            {
                var result = _communicationService.ConnectDeviceAsync(token).Result;
                if (result != null)
                {
                    NameConnectionDevice = System.Text.Encoding.Default.GetString(result);
                }
            });

            if (command)
            {
                task.Start();
            }
            else
            {
                if (task.Status == TaskStatus.Running)
                    tokenSource.Cancel();
            }
        }

        #endregion

        #region Command
        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
