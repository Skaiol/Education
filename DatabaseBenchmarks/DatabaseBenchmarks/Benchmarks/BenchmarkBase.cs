using System.Configuration;
using System.Data.SqlClient;
using BenchmarkDotNet.Attributes;

namespace DatabaseBenchmarks.Benchmarks
{
    [Config(typeof(Config))]
    public abstract class BenchmarkBase
    {
        protected SqlConnection Connection;

        protected static string ConnectionString { get; } =
            ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        protected void BaseSetup()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }
    }
}