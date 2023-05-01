using Microsoft.AspNetCore.Mvc;

namespace ADS.BabySleepSounds.Server.Features.Songs.Song;

public static class SongEndpoint
{
    public static async ValueTask<IResult> GetSong([FromRoute] string songId, CancellationToken cancellationToken)
    {
        var songsResponse = new SongResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(songsResponse);
    }
}
