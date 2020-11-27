using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Services.CommunicationService.Data;
using WeatherStation.Services.CommunicationService.Enum;

namespace WeatherStation.Services.CommunicationService
{
    public interface ICommunicationService
    {
        /// <summary>
        /// Событие, возникающие при приёме данных от прибора.
        /// </summary>
        event EventHandler<DataModel> DataRecived;

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
        /// <param name="cancellationToken">Токен для отмены опреации.</param>
        /// <param name="progress">Прогресс поиска.</param>
        /// <returns>Имя устройства.</returns>
        Task<string> SeachDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null);

        Task<bool> SeachDeviceAsync(CancellationToken cancellationToken, Action<string> callBack, IProgress<byte> progress = null);
    }
}
