using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WeatherStation.BusinessAccess.Sqlite;
using WeatherStation.BusinessAccess.Sqlite.Model;

namespace WeatherStation.Modules.Archives.ViewModels
{
    public class ArchivesViewModel : BindableBase
    {
        #region Filed
        private readonly ISqliteService _sqliteService = null;
        #endregion

        #region Property
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
            var data = await _sqliteService.DeviceRecord.GetAllAsync();
        }
        #endregion
    }
}
