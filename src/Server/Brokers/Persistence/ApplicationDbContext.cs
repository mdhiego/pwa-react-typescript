using BabySounds.Server.Configuration;
using BabySounds.Server.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BabySounds.Server.Brokers.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IHostEnvironment _environment;
    private readonly DbSettings _dbSettings;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<DbSettings> dbOptions,
        IHostEnvironment environment,
        ILogger<ApplicationDbContext> logger
    ) : base(options)
    {
        _dbSettings = dbOptions.Value;
        _environment = environment;

        logger.LogDebug($"{nameof(ApplicationDbContext)} created");
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Playlist> Playlists => Set<Playlist>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_environment.IsDevelopment())
        {
            var dbContextOptionsBuilder = optionsBuilder.UseSqlite(_dbSettings.DefaultConnection);
            dbContextOptionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else
        {
            optionsBuilder.UseNpgsql(_dbSettings.DefaultConnection);
        }
    }
}
