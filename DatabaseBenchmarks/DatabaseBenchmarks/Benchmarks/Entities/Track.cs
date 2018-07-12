using System;
using LinqToDB.Mapping;

namespace DatabaseBenchmarks.Benchmarks.Entities
{
    [Table("Track")]
    public class Track
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Column(Name = "Name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "Duration"), NotNull]
        public int Duration { get; set; }

        [Column(Name = "AlbumId"), NotNull]
        public Guid AlbumId { get; set; }
    }
}