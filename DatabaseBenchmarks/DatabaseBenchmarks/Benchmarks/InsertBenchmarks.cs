using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Dapper;
using DatabaseBenchmarks.Benchmarks.Entities;
using DatabaseBenchmarks.Benchmarks.Providers;
using LinqToDB;
using LinqToDB.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace DatabaseBenchmarks.Benchmarks
{
    public class InsertBenchmarks : BenchmarkBase
    {
        private const int InsertCount = 10000;
        private Configuration _configuration;
        private DataConnection _linq2DbConnection;
        private ISessionFactory _sessionFactory;

        [GlobalSetup]
        public void GlobalSetup()
        {
            BaseSetup();
            DataConnection.DefaultSettings = new Linq2DbSettings(ConnectionString);
            _linq2DbConnection = new DataConnection();
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Program).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [IterationSetup]
        public void Setup()
        {
            _linq2DbConnection.DropTable<Album>();
            _linq2DbConnection.CreateTable<Album>();
        }

        [Benchmark(Description = "Simple Insert")]
        public async Task SimpleInsert()
        {
            for (var i = 0; i < InsertCount; i++)
                await _linq2DbConnection.GetTable<Album>()
                    .InsertAsync(() => new Album
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Name_{i}"
                    });
        }

        [Benchmark(Description = "NHibernate Insert")]
        public async Task NHibernateInsert()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                for (var i = 0; i < InsertCount; i++)
                    await session.Query<Album>()
                        .InsertIntoAsync(x => new Album
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Name_{i}"
                        }, CancellationToken.None);
            }
        }

        [Benchmark(Description = "Bulk Insert")]
        public void BulkInsert()
        {
            var albums = Enumerable.Range(0, InsertCount)
                .Select(x => new Album
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name_{x}"
                }).ToList();
            _linq2DbConnection.BulkCopy(albums);
        }

        [Benchmark(Description = "Dapper Insert")]
        public async Task DapperInsert()
        {
            for (var i = 0; i < InsertCount; i++)
                await Connection.ExecuteAsync(@"
                    insert into Album
                        (Id, Name)
                    values
                        (@Id, @Name)",
                    new Album
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Name_{i}"
                    });
        }
    }
}