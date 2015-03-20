using System.Data.Entity;
using MusicStore.Dal.Entities;
using MusicStore.Dal.Initializers;

namespace MusicStore.Dal
{
    public sealed class MusicStoreContext : DbContext
    {
        static MusicStoreContext()
        {
            Database.SetInitializer(new DefaultInitializer());
        }

        public DbSet<AlbumEntity> AlbumEntities { get; set; }
        public DbSet<ArtistEntity> ArtistEntities { get; set; }
        public DbSet<TrackEntity> TrackEntities { get; set; }
        public DbSet<MusicGenreEntity> MusicGenreEntities { get; set; }
    }
}