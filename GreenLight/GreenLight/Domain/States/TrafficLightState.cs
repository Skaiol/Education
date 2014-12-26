using System;
using System.Threading;
using System.Threading.Tasks;

namespace GreenLight.Domain.States
{
    public abstract class TrafficLightState
    {
        protected readonly ITrafficLight TrafficLight;

        protected TrafficLightState(ITrafficLight trafficLight)
        {
            TrafficLight = trafficLight;
        }

        public TimeSpan Duration { get; set; }
        public TrafficLightState NextState { get; set; }
        protected abstract bool IsDoneCondition { get; }

        public void DoWork()
        {
            Task.Run(() => DoWorkExecute());
        }

        private void DoWorkExecute()
        {
            DoCore();
            SpinWait.SpinUntil(() => IsDoneCondition, Duration);
            DoneCore();

            if (IsDoneCondition)
            {
                return;
            }

            if (NextState != null)
            {
                Task.Run(() => NextState.DoWork());
            }
        }

        protected abstract void DoCore();

        protected virtual void DoneCore()
        {
        }
    }
}