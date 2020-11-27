using Prism.Mvvm;

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

        public ArchivesViewModel()
        {
            Message = "Archives";
        }
    }
}
