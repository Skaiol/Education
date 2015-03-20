using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Dal.Entities
{
    [Table("Artist")]
    public class ArtistEntity : EntityBase
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("MusicGenre")]
        public Guid MusicGenreId { get; set; }
    }
}