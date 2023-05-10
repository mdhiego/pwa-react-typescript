using BabySounds.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Brokers.Persistence;

/// <inheritdoc />
public sealed class ApplicationDbSetup : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private static Playlist? _defaultUserPlaylist;

    public ApplicationDbSetup(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.EnsureCreatedAsync(cancellationToken);

        if (context.Database.IsRelational())
        {
            await context.Database.MigrateAsync(cancellationToken);
        }

        await SeedSampleData(context);
        await SeedDefaultUser(context);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task SeedSampleData(ApplicationDbContext context)
    {
        if (!context.Tracks.Any() && !context.Categories.Any())
        {
            // Categories
            var whiteNoiseCategory = new Category()
            {
                Id = new Guid(),
                Name = "White Noise",
                Description =
                    "White noise is a sound that contains every frequency within the range of human hearing (generally from 20 hertz to 20 kHz) in equal amounts. The term is used frequently in digital audio editing, where white noise is a common type of audio signal that is used to mask unwanted background noise.",
            };
            await context.AddAsync(whiteNoiseCategory);

            // Tracks
            var dryerTrack = new Track()
            {
                Id = new Guid(),
                Title = "Dryer",
                Description = "Dryer spinning",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/dryer.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var fireplaceTrack = new Track()
            {
                Id = new Guid(),
                Title = "Fireplace",
                Description = "Fireplace burning",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/fireplace.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var heartBeatTrack = new Track()
            {
                Id = new Guid(),
                Title = "Heart Beat",
                Description = "Heart beating",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/heart-beat.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var lakeTrack = new Track()
            {
                Id = new Guid(),
                Title = "Lake",
                Description = "Lake water",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/lake.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var rainTrack = new Track()
            {
                Id = new Guid(),
                Title = "Rain",
                Description = "Rain falling",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/rain.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var seaTrack = new Track()
            {
                Id = new Guid(),
                Title = "Sea",
                Description = "Sea waves",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/sea.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var uterusTrack = new Track()
            {
                Id = new Guid(),
                Title = "Uterus",
                Description = "Uterus sounds",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/uterus.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var waterfallTrack = new Track()
            {
                Id = new Guid(),
                Title = "Waterfall",
                Description = "Waterfall sounds",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/waterfall.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var windTrack = new Track()
            {
                Id = new Guid(),
                Title = "Wind",
                Description = "Wind blowing",
                Category = whiteNoiseCategory,
                AudioPath = "tracks/white-noise/wind.mp3",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
            };

            var tracks = new List<Track>()
            {
                dryerTrack,
                fireplaceTrack,
                heartBeatTrack,
                lakeTrack,
                rainTrack,
                seaTrack,
                uterusTrack,
                waterfallTrack,
                windTrack,
            };

            await context.AddRangeAsync(tracks);

            _defaultUserPlaylist = new Playlist()
            {
                Id = new Guid(),
                Name = "White Noise",
                ImagePath = "images/white-noise/default.jpg",
                CreatedAt = DateTime.Now,
                Tracks = tracks,
            };

            await context.AddAsync(_defaultUserPlaylist);

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedDefaultUser(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "TestUser",
                UserName = "test_user",
                Email = "test_user@email.com",
                Password = "password_tests",
                PasswordHash = "password_tests",
                EmailConfirmed = true,
                Playlists = new List<Playlist>()
                {
                    _defaultUserPlaylist!,
                },
            };

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }
}
