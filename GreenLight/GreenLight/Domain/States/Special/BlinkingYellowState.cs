using System;
using System.Timers;

namespace GreenLight.Domain.States.Special
{
    public sealed class BlinkingYellowState : TrafficLightState
    {
        private readonly Timer _timer;

        public BlinkingYellowState(ITrafficLight trafficLight)
            : base(trafficLight)
        {
            _timer = new Timer(TimeSpan.FromSeconds(0.5).Milliseconds);
            _timer.Elapsed += Timer_Elapsed;
        }

        protected override bool IsDoneCondition
        {
            get { return !TrafficLight.IsBlinkingYellow; }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TrafficLight.Yellow.Toggle();
        }

        protected override void DoCore()
        {
            TrafficLight.Green.TurnOff();
            TrafficLight.Red.TurnOff();
            TrafficLight.Yellow.TurnOn();
            _timer.Start();
        }

        protected override void DoneCore()
        {
            _timer.Stop();
        }
    }
}