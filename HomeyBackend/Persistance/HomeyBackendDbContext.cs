using HomeyBackend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeyBackend.Persistance
{
    public class HomeyBackendDbContext(DbContextOptions<HomeyBackendDbContext> options) : IdentityDbContext<UserInfo>(options)
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetailNumberRooms> PlacesDetailNumbers { get; set; }
        public DbSet<PlaceDetailBoolean>PlaceDetailBooleans { get; set; }
        public DbSet<PlaceImage>PlaceImages { get; set; }
        public DbSet<RateToPlace>RateToPlaces{ get; set; }
        public DbSet<Rate>Rates{ get; set; }
        public DbSet<Comment>Comments{ get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Place>()
                .Property(p => p.CreateOn)
                .HasDefaultValueSql("GETUTCDATE()"); 
            builder.Entity<Comment>()
                .Property(p => p.CreateOn)
                .HasDefaultValueSql("GETUTCDATE()");

        }

    }
}
