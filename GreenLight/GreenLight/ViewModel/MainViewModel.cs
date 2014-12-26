using System;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GreenLight.Domain;

namespace GreenLight.ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private TrafficLight _trafficLight;

        public MainViewModel()
        {
            InitTrafficLight();
            InitCommands();
        }

        public SolidColorBrush Green
        {
            get { return _trafficLight.Green.Color; }
        }

        public SolidColorBrush Yellow
        {
            get { return _trafficLight.Yellow.Color; }
        }

        public SolidColorBrush Red
        {
            get { return _trafficLight.Red.Color; }
        }

        public ICommand Enable { get; set; }
        public ICommand Disable { get; set; }
        public ICommand BlinkingYellow { get; set; }

        private void InitTrafficLight()
        {
            _trafficLight = new TrafficLight();
            _trafficLight.Green.StateChanged += Green_StateChanged;
            _trafficLight.Yellow.StateChanged += Yellow_StateChanged;
            _trafficLight.Red.StateChanged += Red_StateChanged;
        }

        private void InitCommands()
        {
            Enable = new RelayCommand(_trafficLight.Enable, () => !_trafficLight.IsEnabled);
            Disable = new RelayCommand(_trafficLight.Disable,
                () => _trafficLight.IsEnabled || _trafficLight.IsBlinkingYellow);
            BlinkingYellow = new RelayCommand(_trafficLight.EnableBlinkingYellow, () => !_trafficLight.IsBlinkingYellow);
        }

        private void Green_StateChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => Green);
        }

        private void Yellow_StateChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => Yellow);
        }

        private void Red_StateChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => Red);
        }
    }
}