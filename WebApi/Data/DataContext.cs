using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;
using WebApi.Models;

namespace WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Photo> Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasKey(u => u.Id); // Configuring primary key
            modelBuilder.Entity<Like>()
                           .HasOne(l => l.User)
                           .WithMany(u => u.Likes)
                           .HasForeignKey(l => l.UserId)
                           .OnDelete(DeleteBehavior.NoAction);
        // Additional configurations if needed
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Like>()
        //        .HasOne(l => l.User)
        //        .WithMany(u => u.Likes)
        //        .HasForeignKey(l => l.UserId)
        //        .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction
        //}
    }
}
