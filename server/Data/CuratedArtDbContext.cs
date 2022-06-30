using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CuratedArt.Data
{
    using CuratedArt.Data.Models;

    public class CuratedArtDbContext : IdentityDbContext
    {
        public CuratedArtDbContext(DbContextOptions<CuratedArtDbContext> options)
            : base(options)
        {

        }

        public DbSet<ArtWork> ArtWorks { get; set; }
    }
}