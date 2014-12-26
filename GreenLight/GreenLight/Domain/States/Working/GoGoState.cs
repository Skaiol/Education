namespace GreenLight.Domain.States.Working
{
    public sealed class GoGoState : WorkingState
    {
        public GoGoState(TrafficLight trafficLight) : base(trafficLight)
        {
        }

        protected override void DoCore()
        {
            TrafficLight.Red.TurnOff();
            TrafficLight.Yellow.TurnOff();
            TrafficLight.Green.TurnOn();
        }
    }
}