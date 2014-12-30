using System.Windows.Media;
using GreenLight.Domain.States;
using GreenLight.Domain.States.Configurators;
using GreenLight.Domain.States.Special;
using GreenLight.Domain.States.Working;

namespace GreenLight.Domain
{
    public sealed class TrafficLight : ITrafficLight
    {
        private TrafficLightState _initialWorkingState;

        public TrafficLight()
        {
            InitLamps();
            InitStates();
            TurnOff();
        }

        public bool IsEnabled { get; private set; }
        public bool IsBlinkingYellow { get; private set; }

        public Lamp Green { get; private set; }
        public Lamp Yellow { get; private set; }
        public Lamp Red { get; private set; }

        private void TurnOff()
        {
            Green.TurnOff();
            Yellow.TurnOff();
            Red.TurnOff();
        }

        private void InitLamps()
        {
            Green = new Lamp(Brushes.Green);
            Yellow = new Lamp(Brushes.Yellow);
            Red = new Lamp(Brushes.Red);
        }

        private void InitStates()
        {
            var stopState = new StopState(this);
            var readyToGoState = new ReadyToGoState(this);
            var goGoState = new GoGoState(this);
            var readyToStopState = new ReadyToStopState(this);
            var goGoIsAlmostDoneState = new GoGoIsAlmostDoneState(this);

            stopState.NextState = readyToGoState;
            readyToGoState.NextState = goGoState;
            goGoState.NextState = goGoIsAlmostDoneState;
            goGoIsAlmostDoneState.NextState = readyToStopState;
            readyToStopState.NextState = stopState;

            StateConfigurator.Instance
                .Configure(stopState)
                .Configure(readyToGoState)
                .Configure(goGoState)
                .Configure(readyToStopState)
                .Configure(goGoIsAlmostDoneState);

            _initialWorkingState = stopState;
        }

        public void Enable()
        {
            IsBlinkingYellow = false;
            IsEnabled = true;
            _initialWorkingState.DoWork();
        }

        public void Disable()
        {
            IsEnabled = false;
            IsBlinkingYellow = false;
            TurnOff();
        }

        public void EnableBlinkingYellow()
        {
            IsBlinkingYellow = true;
            IsEnabled = false;

            var brokenState = new BlinkingYellowState(this);
            StateConfigurator.Instance
                .Configure(brokenState);

            brokenState.DoWork();
        }
    }
}