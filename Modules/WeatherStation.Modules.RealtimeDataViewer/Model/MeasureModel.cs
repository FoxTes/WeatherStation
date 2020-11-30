using System;

namespace WeatherStation.Modules.RealtimeDataViewer.Model
{
    public struct MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
