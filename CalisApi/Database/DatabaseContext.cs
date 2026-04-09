using CalisApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Rutine> Rutines { get; set; }

        public DbSet<RutineExercise> RutineExercises { get; set; }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<SessionAchievement> SessionAchievements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RutineExercise>()
                .HasOne(re => re.Video)
                .WithMany()
                .HasForeignKey(re => re.VideoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}

