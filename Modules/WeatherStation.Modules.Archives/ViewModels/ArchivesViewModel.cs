using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using WeatherStation.BusinessAccess.Sqlite;
using WeatherStation.BusinessAccess.Sqlite.Data;
using WeatherStation.BusinessAccess.Sqlite.Model;

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
            get { return _testBox; }
            set { SetProperty(ref _testBox, value); }
        }

        public DelegateCommand TestReadDatabase { get; private set; }
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

            var temp = data.Select(x => x);

            TestBox = "Закончиили";
        }
        #endregion
    }
}
