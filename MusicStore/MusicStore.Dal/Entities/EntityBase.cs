using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dal.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}