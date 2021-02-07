using System;

namespace WeatherStation.Services.Communication.Model
{
    public class DataReceiveEventArgs : EventArgs
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
