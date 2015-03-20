using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Dal.Entities
{
    [Table("Track")]
    public class TrackEntity : EntityBase
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public byte Number { get; set; }

        [Required]
        [ForeignKey("Album")]
        public Guid AlbumId { get; set; }

        [NotMapped]
        public TimeSpan LengthTime
        {
            get { return TimeSpan.FromSeconds(Length); }
        }
    }
}