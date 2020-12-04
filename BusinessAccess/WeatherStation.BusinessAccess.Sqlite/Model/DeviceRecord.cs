using Prism.Mvvm;

namespace WeatherStation.BusinessAccess.Sqlite.Model
{
    public class DeviceRecord : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private int _temperature;
        public int Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }
    }
}
