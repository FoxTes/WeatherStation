using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using WeatherStation.Core;

namespace WeatherStation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

        private SnackbarMessageQueue _messageQueue;
        public SnackbarMessageQueue MessageQueue
        {
            get { return _messageQueue; }
            set { SetProperty(ref _messageQueue, value); }
        }

        private bool _dialogsIsOpen = false;
        public bool DialogsIsOpen
        {
            get { return _dialogsIsOpen; }
            set { SetProperty(ref _dialogsIsOpen, value); }
        }
        public DelegateCommand SeachDevice { get; private set; }
        public DialogClosingEventHandler DialogClosingHandler { get; }



        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageAnswer>().Subscribe(MessageReceivedAnswer);

            SeachDevice = new DelegateCommand(SendMessage);

            DialogClosingHandler = OnDialogClosing;
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        }


        private void MessageReceivedAnswer(string answer)
        {
            DialogsIsOpen = false;

            if (answer is null)
                MessageQueue.Enqueue("Поиск устройства прерван!");
            else if (string.IsNullOrEmpty(answer))
                MessageQueue.Enqueue("Устройство не найдено.");
            else
                MessageQueue.Enqueue($"Найдено устройство {answer}");
        }

        private void SendMessage()
        {
            DialogsIsOpen = true;
            _eventAggregator.GetEvent<MessageRequest>().Publish(true);
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogsIsOpen = false;
            _eventAggregator.GetEvent<MessageRequest>().Publish(false);
        }
    }
}
