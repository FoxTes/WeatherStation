using System;

namespace WeatherStation.Services.Notification
{
    public class NotificationService : INotificationService
    {
        #region Event
        public event EventHandler<string> MessageRecived;
        #endregion

        #region Method
        public void ShowMessage(string content)
        {
            MessageRecived?.Invoke(this, content);
        }
        #endregion
    }
}
