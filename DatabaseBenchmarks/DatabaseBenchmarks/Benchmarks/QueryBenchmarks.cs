using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Dapper;
using DatabaseBenchmarks.Benchmarks.Entities;
using DatabaseBenchmarks.Benchmarks.Providers;
using LinqToDB;
using LinqToDB.Data;

namespace DatabaseBenchmarks.Benchmarks
{
    public class QueryBenchmarks : BenchmarkBase
    {
        private const int InsertCount = 10000;
        private const int TopCount = 1000;
        private DataConnection _linq2DbConnection;
        private List<Guid> _containsList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            BaseSetup();
            DataConnection.DefaultSettings = new Linq2DbSettings(ConnectionString);
            _linq2DbConnection = new DataConnection();
            _linq2DbConnection.DropTable<Album>();
            _linq2DbConnection.CreateTable<Album>();
            var albums = Enumerable.Range(0, InsertCount)
                .Select(x => new Album
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name_{x}"
                });
            _linq2DbConnection.BulkCopy(albums);
            _containsList = Enumerable.Range(0, TopCount)
                .Select(x => Guid.NewGuid())
                .ToList();
        }


        //[Benchmark(Description = "Simple TOP Query")]
        public async Task<List<Album>> SimpleQuery()
        {
            return await _linq2DbConnection.GetTable<Album>()
                .Take(TopCount)
                .ToListAsync();
        }


        //[Benchmark(Description = "Dapper TOP Query")]
        public async Task<List<Album>> DapperQuery()
        {
            return (await Connection.QueryAsync<Album>($"select TOP {TopCount} * from Album"))
                .ToList();
        }

        //[Benchmark(Description = "Simple Where Query")]
        public async Task<List<Album>> SimpleWhereQuery()
        {
            return await _linq2DbConnection.GetTable<Album>()
                .Where(x => x.Name != "Name_38")
                .ToListAsync();
        }


        //[Benchmark(Description = "Dapper Where Query")]
        public async Task<List<Album>> DapperWhereQuery()
        {
            return (await Connection.QueryAsync<Album>("select * from Album where Name <> 'Name_38'"))
                .ToList();
        }

        [Benchmark(Description = "Simple Where Contains Query")]
        public async Task<List<Album>> SimpleWhereContainsQuery()
        {
            return await _linq2DbConnection.GetTable<Album>()
                .Where(x => _containsList.Contains(x.Id))
                .ToListAsync();
        }


        //[Benchmark(Description = "Dapper Where Contains Query")]
        public async Task<List<Album>> DapperWhereContainsQuery()
        {
            return (await Connection.QueryAsync<Album>("select * from Album where Id in @list", new
                {
                    list = _containsList
                }))
                .ToList();
        }
    }
}