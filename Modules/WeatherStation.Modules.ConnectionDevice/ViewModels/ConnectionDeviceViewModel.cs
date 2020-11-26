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
        private readonly IEventAggregator _eventAggregator = null;
        private readonly ICommunicationService _communicationService = null;

        private string _nameConnectionDevice = null;
        private string _countReciveParcel = null;
        private string _connectionStatus = "Не подключено";

        private CancellationTokenSource _cancellationTokenSource = null;
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

            _eventAggregator.GetEvent<MessageRequest>().Subscribe(RequestSearchDevice);
        }

        private async void RequestSearchDevice(bool request)
        {
            if (request)
            {
                try
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    var result = await _communicationService.SeachDeviceAsync(_cancellationTokenSource.Token);

                    if (result is null)
                    {
                        SetPropertyNoSeachDevice();
                        _eventAggregator.GetEvent<MessageAnswer>().Publish("");
                    }
                    else
                    {
                        SetPropertySeachDevice(result);
                        _eventAggregator.GetEvent<MessageAnswer>().Publish(result);
                    }
                }
                catch (OperationCanceledException)
                {
                    SetPropertyNoSeachDevice();
                    _eventAggregator.GetEvent<MessageAnswer>().Publish(null);
                }
                finally
                {
                    _cancellationTokenSource = null;
                }
            }
            else
                _cancellationTokenSource?.Cancel();
        }
        private void SetPropertyNoSeachDevice()
        {
            NameConnectionDevice = null;
            CountReciveParcel = null;
            ConnectionStatus = "Не подключено";
        }

        private void SetPropertySeachDevice(string result)
        {
            NameConnectionDevice = result;
            CountReciveParcel = "Кол-во принятых посылок: 0";
            ConnectionStatus = "Устройство подключено";
        }
        #endregion
    }
}
