using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using Serilog;
using System;
using WeatherStation.Core;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Services.CommunicationService.Enum;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Filed
        private readonly ILoggerFacade _logger = null;
        private readonly IRegionManager _regionManager = null;
        private readonly IEventAggregator _eventAggregator = null;
        private readonly ICommunicationService _communicationService = null;

        private bool _dialogsIsOpen = false;
        private bool _darkModeIsEnable = true;
        #endregion

        #region Property
        public SnackbarMessageQueue MessageQueue { get; private set; }

        public DelegateCommand SeachDevice { get; private set; }

        public DelegateCommand ChangeTheme { get; private set; }
        public bool DarkModeIsEnable
        {
            get => _darkModeIsEnable; 
            set => SetProperty(ref _darkModeIsEnable, value); 
        }

        public DialogClosingEventHandler DialogClosingHandler { get; }
        public bool DialogsIsOpen
        {
            get => _dialogsIsOpen; 
            set => SetProperty(ref _dialogsIsOpen, value);
        }

        private int _selectItem;
        public int SelectItem
        {
            get 
            {
                switch (_selectItem)
                {
                    case 0:
                        _regionManager.RequestNavigate(RegionNames.MainContent, "RealtimeDataViewer");
                        break;
                    case 1:
                        _regionManager.RequestNavigate(RegionNames.MainContent, "Archives");
                        break;
                }
                return _selectItem; 
            }
            set { SetProperty(ref _selectItem, value); }
        }
        #endregion
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ICommunicationService communicationService, ILoggerFacade logger)
        {
            _logger = logger;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            _communicationService = communicationService;
            _communicationService.ConnectionChanged += ConnectionChanged;

            SeachDevice = new DelegateCommand(SendMessage);
            ChangeTheme = new DelegateCommand(ModifyTheme);

            DialogClosingHandler = OnDialogClosing;
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        }

        private void ConnectionChanged(object sender, ConnectionStatus e)
        {
            if (DialogsIsOpen)
            {
                DialogsIsOpen = false;

                if (e == ConnectionStatus.Connect)
                    MessageQueue.Enqueue($"Найдено устройство.");
                else
                    MessageQueue.Enqueue("Устройство не найдено.");
            }
            else
                if (e == ConnectionStatus.Disconnect)
                    MessageQueue.Enqueue($"Связь с устройством потеряна!");
        }

        private void SendMessage()
        {
            if (_communicationService.IsConnected)
                MessageQueue.Enqueue($"Устройство уже подключено!");
            else
            {
                DialogsIsOpen = true;

                _logger.Log("Начат поиск устройства.", Category.Info, Priority.Low);
                _eventAggregator.GetEvent<MessageRequest>().Publish(true);
            }
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogsIsOpen = false;

            MessageQueue.Enqueue("Поиск устройства прерван!");
            _eventAggregator.GetEvent<MessageRequest>().Publish(false);
        }

        private void ModifyTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(DarkModeIsEnable ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
