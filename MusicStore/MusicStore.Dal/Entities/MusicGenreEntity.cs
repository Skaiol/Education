using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Dal.Entities
{
    [Table("MusicGenre")]
    public class MusicGenreEntity : EntityBase
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}