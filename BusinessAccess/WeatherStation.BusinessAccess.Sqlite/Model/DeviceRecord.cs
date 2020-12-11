﻿using System;

namespace WeatherStation.BusinessAccess.Sqlite.Model
{
    public class DeviceRecord
    {
        public int Id { get; set; }  
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}
