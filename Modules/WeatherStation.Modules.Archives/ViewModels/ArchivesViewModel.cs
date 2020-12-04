using Prism.Mvvm;
using WeatherStation.BusinessAccess.Sqlite;
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.Modules.Archives.ViewModels
{
    public class ArchivesViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ArchivesViewModel(ISqliteService sqliteService)
        {
            Message = "Archives";

            sqliteService.DeviceRecord.RecordSaved(new DeviceRecord { Temperature = 20 });
        }
    }
}
