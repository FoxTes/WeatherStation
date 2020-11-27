using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using WeatherStation.Core;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Services.CommunicationService.Enum;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Filed
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
            get { return _darkModeIsEnable; }
            set { SetProperty(ref _darkModeIsEnable, value); }
        }

        public DialogClosingEventHandler DialogClosingHandler { get; }
        public bool DialogsIsOpen
        {
            get { return _dialogsIsOpen; }
            set { SetProperty(ref _dialogsIsOpen, value); }
        }
        #endregion

        public MainWindowViewModel(IEventAggregator eventAggregator, ICommunicationService communicationService)
        {
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
