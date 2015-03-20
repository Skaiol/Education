using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Dal.Entities
{
    [Table("Album")]
    public class AlbumEntity : EntityBase
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [ForeignKey("Artist")]
        public Guid ArtistId { get; set; }
    }
}