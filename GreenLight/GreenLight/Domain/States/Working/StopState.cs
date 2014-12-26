namespace GreenLight.Domain.States.Working
{
    public sealed class StopState : WorkingState
    {
        public StopState(TrafficLight trafficLight) : base(trafficLight)
        {
        }

        protected override void DoCore()
        {
            TrafficLight.Red.TurnOn();
            TrafficLight.Yellow.TurnOff();
            TrafficLight.Green.TurnOff();
        }
    }
}