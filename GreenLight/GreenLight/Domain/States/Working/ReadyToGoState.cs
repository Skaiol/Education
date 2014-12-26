namespace GreenLight.Domain.States.Working
{
    public sealed class ReadyToGoState : WorkingState
    {
        public ReadyToGoState(TrafficLight trafficLight) : base(trafficLight)
        {
        }

        protected override void DoCore()
        {
            TrafficLight.Red.TurnOn();
            TrafficLight.Yellow.TurnOn();
            TrafficLight.Green.TurnOff();
        }
    }
}