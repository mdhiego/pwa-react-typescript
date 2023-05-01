using ADS.BabySleepSounds.Server.Features.Songs.Catalog;
using ADS.BabySleepSounds.Server.Features.Songs.Song;

namespace ADS.BabySleepSounds.Server.Features.Songs;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapSongsEndpoints(this IEndpointRouteBuilder app)
    {
        var songs = app
            .MapGroup("/songs")
            .RequireAuthorization()
            .WithTags(nameof(Songs));
        {
            songs
                .MapGet("catalog", CatalogEndpoint.GetCatalog)
                .Produces<CatalogResponse>(StatusCodes.Status200OK);

            songs
                .MapGet("{songId}", SongEndpoint.GetSong)
                .Produces<CatalogResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
