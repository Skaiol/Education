namespace GreenLight.Domain.States.Working
{
    public abstract class WorkingState : TrafficLightState
    {
        protected WorkingState(ITrafficLight trafficLight)
            : base(trafficLight)
        {
        }

        protected override bool IsDoneCondition
        {
            get { return !TrafficLight.IsEnabled; }
        }
    }
}