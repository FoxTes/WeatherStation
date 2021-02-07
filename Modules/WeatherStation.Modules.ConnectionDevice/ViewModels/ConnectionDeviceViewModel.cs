using Prism.Events;
using Prism.Regions;
using System;
using System.Threading;
using WeatherStation.Core;
using WeatherStation.Core.Mvvm;
using WeatherStation.Services.Communication;
using WeatherStation.Services.Communication.Enum;
using WeatherStation.Services.Communication.Model;

namespace WeatherStation.Modules.ConnectionDevice.ViewModels
{
    public class ConnectionDeviceViewModel : RegionViewModelBase
    {
        #region Filed
        private readonly IEventAggregator _eventAggregator;
        private readonly ICommunicationService _communicationService;

        private string _nameConnectionDeviceLabel;
        private string _countReciveFrameLabel;
        private string _connectionStatusLabel = "Не подключено";
        private int _countReciveFrame;

        private CancellationTokenSource _cancellationTokenSource;
        #endregion

        #region Property
        public string NameConnectionDeviceLabel
        {
            get => _nameConnectionDeviceLabel;
            set => SetProperty(ref _nameConnectionDeviceLabel, value);
        }

        public string ConnectionStatusLabel
        {
            get => _connectionStatusLabel;
            set => SetProperty(ref _connectionStatusLabel, value);
        }

        public string CountReciveFrameLabel
        {
            get => _countReciveFrameLabel;
            set => SetProperty(ref _countReciveFrameLabel, value);
        }
        #endregion

        #region Constructor
        public ConnectionDeviceViewModel(IRegionManager regionManager, ICommunicationService communicationService, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageRequest>().Subscribe(RequestSearchDevice);

            _communicationService = communicationService;
            _communicationService.DataReceived += DataReceived;
            _communicationService.ConnectionChanged += ConnectionChanged;
        }
        #endregion

        #region Method
        private void ConnectionChanged(object sender, ConnectionStatus e)
        {
            if (e != ConnectionStatus.Disconnect) 
                return;

            _countReciveFrame = 0;
            SetPropertyNoSearchDevice();
        }

        private void DataReceived(object sender, DataReceiveEventArgs e)
        {
            CountReciveFrameLabel = $"Кол-во принятых посылок: {++_countReciveFrame}";
        }

        private async void RequestSearchDevice(bool request)
        {
            if (request)
                try
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    var result = await _communicationService.SearchDeviceAsync(_cancellationTokenSource.Token);

                    if (result is null)
                        SetPropertyNoSearchDevice();
                    else
                        SetPropertySearchDevice(result);
                }
                catch (OperationCanceledException)
                {
                    SetPropertyNoSearchDevice();
                }
                finally
                {
                    _cancellationTokenSource = null;
                }
            else
                _cancellationTokenSource?.Cancel();
        }

        private void SetPropertyNoSearchDevice()
        {
            NameConnectionDeviceLabel = null;
            CountReciveFrameLabel = null;
            ConnectionStatusLabel = "Не подключено";
        }

        private void SetPropertySearchDevice(string result)
        {
            NameConnectionDeviceLabel = result;
            CountReciveFrameLabel = $"Кол-во принятых посылок: {_countReciveFrame}";
            ConnectionStatusLabel = "Устройство подключено";
        }
        #endregion
    }
}
