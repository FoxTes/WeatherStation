using System;

namespace WeatherStation.Services.CommunicationService.Model
{
    public class DataReciveEventArgs : EventArgs
    {
        /// <summary>
        /// Температура воздуха.
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Атмосферное давление.
        /// </summary>
        public float AtmosphericPressure { get; set; }

        /// <summary>
        /// Влажность воздуха.
        /// </summary>
        public float Humidity { get; set; }
    }
}
