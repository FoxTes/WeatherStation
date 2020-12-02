using Prism.Events;
using Prism.Regions;
using System;
using System.Threading;
using WeatherStation.Core;
using WeatherStation.Core.Mvvm;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Services.CommunicationService.Enum;
using WeatherStation.Services.CommunicationService.Model;

namespace WeatherStation.Modules.ConnectionDevice.ViewModels
{
    public class ConnectionDeviceViewModel : RegionViewModelBase
    {
        #region Filed
        private readonly IEventAggregator _eventAggregator = null;
        private readonly ICommunicationService _communicationService = null;

        private string _nameConnectionDeviceLabel = null;
        private string _countReciveFrameLabel = null;
        private string _connectionStatusLabel = "Не подключено";
        private int _countReciveFrame = 0;

        private CancellationTokenSource _cancellationTokenSource = null;
        #endregion

        #region Property
        public string NameConnectionDeviceLabel
        {
            get { return _nameConnectionDeviceLabel; }
            set { SetProperty(ref _nameConnectionDeviceLabel, value); }
        }

        public string ConnectionStatusLabel
        {
            get { return _connectionStatusLabel; }
            set { SetProperty(ref _connectionStatusLabel, value); }
        }

        public string CountReciveFrameLabel
        {
            get { return _countReciveFrameLabel; }
            set { SetProperty(ref _countReciveFrameLabel, value); }
        }
        #endregion

        #region Constructor
        public ConnectionDeviceViewModel(IRegionManager regionManager, ICommunicationService communicationService, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageRequest>().Subscribe(RequestSearchDevice);

            _communicationService = communicationService;
            _communicationService.DataRecived += DataRecived;
            _communicationService.ConnectionChanged += ConnectionChanged;
        }
        #endregion

        #region Method
        private void ConnectionChanged(object sender, ConnectionStatus e)
        {
            if (e == ConnectionStatus.Disconnect)
            {
                _countReciveFrame = 0;
                SetPropertyNoSeachDevice();
            }        
        }

        private void DataRecived(object sender, DataReciveDto e)
        {
            CountReciveFrameLabel = $"Кол-во принятых посылок: {++_countReciveFrame}";
        }

        private async void RequestSearchDevice(bool request)
        {
            if (request)
                try
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    var result = await _communicationService.SeachDeviceAsync(_cancellationTokenSource.Token);

                    if (result is null)
                        SetPropertyNoSeachDevice();
                    else
                        SetPropertySeachDevice(result);
                }
                catch (OperationCanceledException)
                {
                    SetPropertyNoSeachDevice();
                }
                finally
                {
                    _cancellationTokenSource = null;
                }
            else
                _cancellationTokenSource?.Cancel();
        }

        private void SetPropertyNoSeachDevice()
        {
            NameConnectionDeviceLabel = null;
            CountReciveFrameLabel = null;
            ConnectionStatusLabel = "Не подключено";
        }

        private void SetPropertySeachDevice(string result)
        {
            NameConnectionDeviceLabel = result;
            CountReciveFrameLabel = $"Кол-во принятых посылок: {_countReciveFrame}";
            ConnectionStatusLabel = "Устройство подключено";
        }
        #endregion
    }
}
