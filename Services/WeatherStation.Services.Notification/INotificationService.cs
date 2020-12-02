using System;

namespace WeatherStation.Services.Notification
{
    public interface INotificationService
    {
        /// <summary>
        /// Событие, возникающие при возникновении уведомления.
        /// </summary>
        event EventHandler<string> MessageRecived;

        /// <summary>
        /// Показать уведомление на основной форме.
        /// </summary>
        /// <param name="content">Информация о сообщении.</param>
        void ShowMessage(string content);
    }
}
