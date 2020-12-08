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

            sqliteService.DeviceRecord.Add(new DeviceRecord() {Temperature = 20 });

            var temp = sqliteService.DeviceRecord.GetAll();
            var data = sqliteService.DeviceRecord.GetSingle(x => x.Id == 2);

        }
    }
}
