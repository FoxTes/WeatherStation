using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using WeatherStation.Core;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _ea;

        private bool _dialogsIsOpen = false;
        public bool DialogsIsOpen
        {
            get { return _dialogsIsOpen; }
            set { SetProperty(ref _dialogsIsOpen, value); }
        }
        public DelegateCommand SeachDevice { get; private set; }
        public DialogClosingEventHandler DialogClosingHandler { get; }

        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SeachDevice = new DelegateCommand(SendMessage);
            DialogClosingHandler = OnDialogClosing;
        }

        private void SendMessage()
        {
            DialogsIsOpen = true;
            _ea.GetEvent<MessageSentEvent>().Publish(DialogsIsOpen);
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogsIsOpen = false;
            _ea.GetEvent<MessageSentEvent>().Publish(DialogsIsOpen);
        }
    }
}
