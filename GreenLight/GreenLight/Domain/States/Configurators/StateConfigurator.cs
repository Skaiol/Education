using System;
using GreenLight.Domain.States.Special;
using GreenLight.Domain.States.Working;
using GreenLight.Properties;

namespace GreenLight.Domain.States.Configurators
{
    public sealed class StateConfigurator
    {
        public StateConfigurator Configure(ReadyToGoState state)
        {
            state.Duration = Settings.Default.ReadyToGoState;
            return this;
        }

        public StateConfigurator Configure(GoGoState state)
        {
            state.Duration = Settings.Default.GoGoState;
            return this;
        }

        public StateConfigurator Configure(StopState state)
        {
            state.Duration = Settings.Default.StopState;
            return this;
        }

        public StateConfigurator Configure(ReadyToStopState state)
        {
            state.Duration = Settings.Default.ReadyToStopState;
            return this;
        }

        public StateConfigurator Configure(GoGoIsAlmostDoneState state)
        {
            state.Duration = Settings.Default.GoGoIsAlmostDoneState;
            return this;
        }

        public StateConfigurator Configure(BlinkingYellowState state)
        {
            state.Duration = TimeSpan.FromTicks(int.MaxValue);
            return this;
        }
    }
}