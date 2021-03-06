﻿using LiveCharts;
using LiveCharts.Configurations;
using Prism.Mvvm;
using System;
using WeatherStation.Modules.RealtimeDataViewer.Model;
using WeatherStation.Services.Communication;
using WeatherStation.Services.Communication.Model;

namespace WeatherStation.Modules.RealtimeDataViewer.ViewModels
{
    public class RealtimeDataViewerViewModel : BindableBase
    {
        #region Filed
        private double _temperatureValue = 0;
        private double _pressureValue = 0;
        private double _humidityValue = 0;
        private double _axisMax = 0;
        private double _axisMin = 0;
        #endregion

        public double TemperatureValue
        {
            get => _temperatureValue; 
            set => SetProperty(ref _temperatureValue, value);
        }
        public double PressureValue
        {
            get => _pressureValue;
            set => SetProperty(ref _pressureValue, value);
        }
        public double HumidityValue
        {
            get => _humidityValue;
            set => SetProperty(ref _humidityValue, value);
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
            communicationService.DataReceived += DataRecivedEvent;

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

        private void DataRecivedEvent(object sender, DataReceiveEventArgs e)
        {
            TemperatureValue = e.Temperature;
            PressureValue = e.AtmosphericPressure;
            HumidityValue = e.Humidity;

            ChartValues.Add(new MeasureModel
            {
                DateTime = DateTime.Now,
                Value = e.Temperature
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
