using Prism.Mvvm;

namespace WeatherStation.Modules.RealtimeDataViewer.ViewModels
{
    public class RealtimeDataViewerViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public RealtimeDataViewerViewModel()
        {
            Message = "RealtimeDataViewer";
        }
    }
}
