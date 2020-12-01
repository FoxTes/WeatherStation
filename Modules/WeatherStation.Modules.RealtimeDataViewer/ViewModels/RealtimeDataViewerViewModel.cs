using LiveCharts;
using LiveCharts.Configurations;
using Prism.Mvvm;
using System;
using WeatherStation.Modules.RealtimeDataViewer.Model;
using WeatherStation.Services.CommunicationService;
using WeatherStation.Services.CommunicationService.Model;

namespace WeatherStation.Modules.RealtimeDataViewer.ViewModels
{
    public class RealtimeDataViewerViewModel : BindableBase
    {
        #region Filed
        private readonly ICommunicationService _communicationService = null;

        private double _temperatureValue = 0;
        private double _axisMax = 0;
        private double _axisMin = 0;
        #endregion

        public double TemperatureValue
        {
            get => _temperatureValue; 
            set => SetProperty(ref _temperatureValue, value);
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public double AxisMax
        {
            get => _axisMax;
            set => SetProperty(ref _axisMax, value);
        }
        public double AxisMin
        {
            get => _axisMin;
            set => SetProperty(ref _axisMin, value);
        }

        #region Constructor
        public RealtimeDataViewerViewModel(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
            _communicationService.DataRecived += DataRecived;

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   
                .Y(model => model.Value);
            Charting.For<MeasureModel>(mapper);

            ChartValues = new ChartValues<MeasureModel>();
            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");
            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;
            SetAxisLimits(DateTime.Now);
        }
        #endregion

        private void DataRecived(object sender, DataReciveDto e)
        {
            TemperatureValue = e.MainTemperature;
            ChartValues.Add(new MeasureModel
            {
                DateTime = DateTime.Now,
                Value = e.MainTemperature
            });

            SetAxisLimits(DateTime.Now);
            if (ChartValues.Count > 150) 
                ChartValues.RemoveAt(0);
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(30).Ticks; // and 8 seconds behind
        }
    }
}
