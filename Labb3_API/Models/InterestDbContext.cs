using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Models
{
    public class InterestDbContext : DbContext
    {
        public InterestDbContext(DbContextOptions<InterestDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
           new User { Id = 1, FullName = "Anna Svensson", Email = "anna.svensson@gmail.com", Phone = "0701234567" },
                new User { Id = 2, FullName = "Erik Johansson", Email = "erik.johansson@hotmail.com", Phone = "0729876543" },
                new User { Id = 3, FullName = "Sara Andersson", Email = "sara.andersson@gmail.com", Phone = "0731122334" }
            );

            modelBuilder.Entity<Interest>().HasData(
           new Interest { Id = 1, Name = "Programmering", Description = "Att skriva kod och utveckla applikationer" },
                new Interest { Id = 2, Name = "Träning", Description = "Fysisk aktivitet som gym, löpning eller sport" },
                new Interest { Id = 3, Name = "Musik", Description = "Lyssna på eller skapa musik" },
                new Interest { Id = 4, Name = "Resor", Description = "Utforska nya platser och kulturer" },
                new Interest { Id = 5, Name = "Matlagning", Description = "Laga och experimentera med mat" },
                new Interest { Id = 6, Name = "Gaming", Description = "Spela dator- eller tv-spel" },
                new Interest { Id = 7, Name = "Läsning", Description = "Läsa böcker, artiklar eller annan litteratur" }
            );

            modelBuilder.Entity<Link>().HasData(
                // Anna
           new Link { Id = 1, Url = "https://github.com", UserId = 1, InterestId = 1 }, // Programmering
                new Link { Id = 2, Url = "https://spotify.com", UserId = 1, InterestId = 3 }, // Musik

                // Erik
                new Link { Id = 3, Url = "https://gymshark.com", UserId = 2, InterestId = 2 }, // Träning
                new Link { Id = 4, Url = "https://twitch.tv", UserId = 2, InterestId = 6 }, // Gaming

                // Sara
                new Link { Id = 5, Url = "https://tripadvisor.com", UserId = 3, InterestId = 4 }, // Resor
                new Link { Id = 6, Url = "https://foodblog.com", UserId = 3, InterestId = 5 }, // Matlagning
                new Link { Id = 7, Url = "https://goodreads.com", UserId = 3, InterestId = 7 } // Läsning
            );
        }
    }
}
