using ADS.BabySleepSounds.Server.Configuration;
using ADS.BabySleepSounds.Server.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ADS.BabySleepSounds.Server.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly DbSettings _dbSettings;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<DbSettings> dbOptions,
        ILogger<ApplicationDbContext> logger) : base(options)
    {
        _dbSettings = dbOptions.Value;

        logger.LogDebug($"{nameof(ApplicationDbContext)} created");
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Sound> Sounds => Set<Sound>();
    public DbSet<Playlist> Playlists => Set<Playlist>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // use local sqlite db
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_dbSettings.DefaultConnection);
    }
}
