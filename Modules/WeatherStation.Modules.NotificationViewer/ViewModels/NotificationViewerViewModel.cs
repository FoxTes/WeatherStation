using System;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using WeatherStation.Services.Notification;

namespace WeatherStation.Modules.NotificationViewer.ViewModels
{
    public class NotificationViewerViewModel : BindableBase
    {
        #region Filed
        private readonly INotificationService _notificationService = null;
        #endregion

        #region Property
        public SnackbarMessageQueue MessageQueue { get; private set; }
        #endregion

        #region Contructor
        public NotificationViewerViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _notificationService.MessageRecived += MessageRecivedEvent;

            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        }
        #endregion

        #region Method
        private void MessageRecivedEvent(object sender, string content)
        {
            MessageQueue.Enqueue(content);
        }
        #endregion
    }
}
