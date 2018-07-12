using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace DatabaseBenchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            Add(Job.Default
                //.With(RunStrategy.ColdStart)
                .WithLaunchCount(1)
                .WithWarmupCount(1)
                .WithTargetCount(4)
                .WithRemoveOutliers(true)
            );
        }
    }
}