using System;
using System.Timers;

namespace GreenLight.Domain.States.Working
{
    public sealed class GoGoIsAlmostDoneState : WorkingState
    {
        private readonly Timer _timer;

        public GoGoIsAlmostDoneState(TrafficLight trafficLight)
            : base(trafficLight)
        {
            _timer = new Timer(TimeSpan.FromSeconds(0.5).Milliseconds);
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TrafficLight.Green.Toggle();
        }

        protected override void DoCore()
        {
            TrafficLight.Yellow.TurnOff();
            TrafficLight.Red.TurnOff();
            TrafficLight.Green.TurnOn();
            _timer.Start();
        }

        protected override void DoneCore()
        {
            _timer.Stop();
        }
    }
}