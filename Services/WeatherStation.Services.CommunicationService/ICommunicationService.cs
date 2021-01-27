using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Services.CommunicationService.Enum;
using WeatherStation.Services.CommunicationService.Model;

namespace WeatherStation.Services.CommunicationService
{
    public interface ICommunicationService
    {
        /// <summary>
        /// Событие, возникающие при приеме данных от прибора.
        /// </summary>
        event EventHandler<DataReciveEventArgs> DataRecived;

        /// <summary>
        /// Событие, возникающие при изменении статуса подключения прибора.
        /// </summary>
        event EventHandler<ConnectionStatus> ConnectionChanged;

        /// <summary>
        /// Статус подключения.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Поиск подключенного прибора по COM порту.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        /// <param name="progress">Прогресс поиска.</param>
        /// <returns>Имя устройства.</returns>
        Task<string> SeachDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null);
    }
}
