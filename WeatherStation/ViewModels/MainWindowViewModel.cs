using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using WeatherStation.Core;
using WeatherStation.Services.Communication;
using WeatherStation.Services.Communication.Enum;
using WeatherStation.Services.Notification;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Filed
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly ICommunicationService _communicationService;

        private bool _dialogsIsOpen;
        private bool _darkModeIsEnable = true;
        #endregion

        #region Property
        public SnackbarMessageQueue MessageQueue { get; private set; }

        public DelegateCommand SeachDevice { get; }

        public DelegateCommand ChangeTheme { get; }
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
            set => SetProperty(ref _selectItem, value);
        }
        #endregion

        #region Constructor
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, 
                                   ICommunicationService communicationService, INotificationService notificationService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;

            _communicationService = communicationService;
            _communicationService.ConnectionChanged += ConnectionChangedEvent;

            SeachDevice = new DelegateCommand(SendMessage);
            ChangeTheme = new DelegateCommand(ModifyTheme);

            DialogClosingHandler = OnDialogClosing;
        }
        #endregion

        #region Method
        private void ConnectionChangedEvent(object sender, ConnectionStatus e)
        {
            if (DialogsIsOpen)
            {
                DialogsIsOpen = false;

                _notificationService.ShowMessage(e == ConnectionStatus.Connect
                    ? "Найдено устройство."
                    : "Устройство не найдено.");
            }
            else
                if (e == ConnectionStatus.Disconnect)
                    _notificationService.ShowMessage("Связь с устройством потеряна!");
        }

        private void SendMessage()
        {
            if (_communicationService.IsConnected)
                _notificationService.ShowMessage("Устройство уже подключено!");
            else
            {
                DialogsIsOpen = true;

                _eventAggregator.GetEvent<MessageRequest>().Publish(true);
            }
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogsIsOpen = false;

            _notificationService.ShowMessage("Поиск устройства прерван!");
            _eventAggregator.GetEvent<MessageRequest>().Publish(false);
        }

        private void ModifyTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(DarkModeIsEnable ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);

            _notificationService.ShowMessage("Старт приложения!");
        }
        #endregion
    }
}
