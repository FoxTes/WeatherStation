using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Services.Communication.Enum;
using WeatherStation.Services.Communication.Model;

namespace WeatherStation.Services.Communication
{
    public interface ICommunicationService
    {
        /// <summary>
        /// Событие, возникающие при приеме данных от прибора.
        /// </summary>
        event EventHandler<DataReceiveEventArgs> DataReceived;

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
        Task<string> SearchDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null);
    }
}
