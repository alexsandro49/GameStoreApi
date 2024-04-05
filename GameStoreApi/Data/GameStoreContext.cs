using GameStoreApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Casual" },
            new { Id = 3, Name = "Fighting" },
            new { Id = 4, Name = "Music" },
            new { Id = 5, Name = "Racing" },
            new { Id = 6, Name = "RPG" },
            new { Id = 7, Name = "Shooter" },
            new { Id = 8, Name = "Sports" }
        );
    }
}