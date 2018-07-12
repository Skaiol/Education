using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace DatabaseBenchmarks.Benchmarks.Providers
{
    public class Linq2DbSettings : ILinqToDBSettings
    {
        private readonly string _connectionString;

        public Linq2DbSettings(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "SqlServer",
                        ProviderName = "SqlServer",
                        ConnectionString = _connectionString
                    };
            }
        }
    }
}