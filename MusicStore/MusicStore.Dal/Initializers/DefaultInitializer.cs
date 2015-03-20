using System;
using System.Data.Entity;
using MusicStore.Dal.Entities;
using Nelibur.Sword.Core;

namespace MusicStore.Dal.Initializers
{
    public sealed class DefaultInitializer : DropCreateDatabaseIfModelChanges<MusicStoreContext>
    {
        private static readonly Guid _rock = Guid.NewGuid();
        private static readonly Guid _metal = Guid.NewGuid();
        private static readonly Guid _jazz = Guid.NewGuid();
        private static readonly Guid _pop = Guid.NewGuid();

        private static readonly Guid _queen = GuidComb.New();

        private static readonly Guid _innuendo = GuidComb.New();

        protected override void Seed(MusicStoreContext context)
        {
            AddGenres(context);
            AddArtists(context);
            AddAlbums(context);
            AddTracks(context);
        }

        private static void AddTracks(MusicStoreContext context)
        {
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Innuendo",
                Length = 389
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "I'm Going Slightly Mad",
                Length = 262
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Headlong",
                Length = 279
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "I Can't Live with You",
                Length = 275
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Don't Try So Hard",
                Length = 209
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Ride the Wild Wind",
                Length = 281
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "All God's People",
                Length = 259
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "These Are the Days of Our Lives",
                Length = 252
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Delilah",
                Length = 212
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "The Hitman",
                Length = 292
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "Bijou",
                Length = 216
            });
            context.TrackEntities.Add(new TrackEntity
            {
                Id = GuidComb.New(),
                AlbumId = _innuendo,
                Name = "The Show Must Go On",
                Length = 264
            });
        }

        private static void AddAlbums(MusicStoreContext context)
        {
            context.AlbumEntities.Add(new AlbumEntity
            {
                Id = _innuendo,
                ArtistId = _queen,
                Name = "Innuendo",
                ReleaseYear = 1991
            });
        }

        private static void AddArtists(MusicStoreContext context)
        {
            context.ArtistEntities.Add(new ArtistEntity {Id = _queen, Name = "Queen", MusicGenreId = _rock});
        }

        private static void AddGenres(MusicStoreContext context)
        {
            context.MusicGenreEntities.Add(new MusicGenreEntity {Id = _metal, Name = "Metal"});
            context.MusicGenreEntities.Add(new MusicGenreEntity {Id = _rock, Name = "Rock"});
            context.MusicGenreEntities.Add(new MusicGenreEntity {Id = _jazz, Name = "Jazz"});
            context.MusicGenreEntities.Add(new MusicGenreEntity {Id = _pop, Name = "Pop"});
        }
    }
}