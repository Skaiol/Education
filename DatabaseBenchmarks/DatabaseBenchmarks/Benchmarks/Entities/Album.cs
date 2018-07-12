using System;
using LinqToDB.Mapping;

namespace DatabaseBenchmarks.Benchmarks.Entities
{
    [Table("Album")]
    public class Album
    {
        [PrimaryKey]
        public virtual Guid Id { get; set; }

        [Column(Name = "Name"), NotNull]
        public virtual string Name { get; set; }
    }
}