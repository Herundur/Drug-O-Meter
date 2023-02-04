using Drug_O_Meter.Core;

namespace Drug_O_Meter.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AlcoholViewCommand { get; set; }
        public RelayCommand CannabisViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public AlcoholViewModel AlcoholVM { get; set; }
        public CannabisViewModel CannabisVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPorpertyChanged();
            }
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            AlcoholVM = new AlcoholViewModel();
            CannabisVM = new CannabisViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            { 
                CurrentView = HomeVM;
            });

            AlcoholViewCommand = new RelayCommand(o =>
            {
                CurrentView = AlcoholVM;
            });

            CannabisViewCommand = new RelayCommand(o =>
            {
                CurrentView = CannabisVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

        }
    }
}
