using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Services.CommunicationService
{
    public interface ICommunicationService
    {
        /// <summary>
        /// Поиск подключенного прибора.
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены опреации.</param>
        /// <param name="progress">Прогресс поиска.</param>
        /// <returns>Имя устройства.</returns>
        Task<string> SeachDeviceAsync(CancellationToken cancellationToken, IProgress<byte> progress = null);
    }
}
