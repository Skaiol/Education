namespace GreenLight.Domain.States.Working
{
    public sealed class ReadyToStopState : WorkingState
    {
        public ReadyToStopState(TrafficLight trafficLight) : base(trafficLight)
        {
        }

        protected override void DoCore()
        {
            TrafficLight.Red.TurnOff();
            TrafficLight.Yellow.TurnOn();
            TrafficLight.Green.TurnOff();
        }
    }
}