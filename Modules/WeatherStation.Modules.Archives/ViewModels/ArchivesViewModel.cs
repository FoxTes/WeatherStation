using Prism.Commands;
using Prism.Mvvm;
using System.Linq;
using WeatherStation.BusinessAccess.Sqlite;

namespace WeatherStation.Modules.Archives.ViewModels
{
    public class ArchivesViewModel : BindableBase
    {
        #region Filed
        private readonly ISqliteService _sqliteService;

        private string _testBox;
        #endregion

        #region Property
        public string TestBox
        {
            get => _testBox;
            set => SetProperty(ref _testBox, value);
        }

        public DelegateCommand TestReadDatabase { get; }
        #endregion

        #region Constructor
        public ArchivesViewModel(ISqliteService sqliteService)
        {
            _sqliteService = sqliteService;

            TestReadDatabase = new DelegateCommand(ReadEntity);
        }
        #endregion

        #region Method
        private async void ReadEntity()
        {
            TestBox = "Начали";

            var data = await _sqliteService.DeviceRecord.GetAllAsync();
            var data1 = await _sqliteService.DeviceRecord.GetAllAsync();

            var temp = data.Select(x => x);
            var temp1 = data1.Select(x => x);

            TestBox = "Закончили";
        }
        #endregion
    }
}
