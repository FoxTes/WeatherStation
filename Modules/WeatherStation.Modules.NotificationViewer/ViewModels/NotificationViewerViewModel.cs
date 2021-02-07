using System;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using WeatherStation.Services.Notification;

namespace WeatherStation.Modules.NotificationViewer.ViewModels
{
    public class NotificationViewerViewModel : BindableBase
    {
        #region Filed
        private readonly INotificationService _notificationService;
        #endregion

        #region Property
        public SnackbarMessageQueue MessageQueue { get; }
        #endregion

        #region Contructor
        public NotificationViewerViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _notificationService.MessageRecived += MessageReceivedEvent;

            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        }
        #endregion

        #region Method
        private void MessageReceivedEvent(object sender, string content)
        {
            MessageQueue.Enqueue(content);
        }
        #endregion
    }
}
