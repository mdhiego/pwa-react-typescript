namespace ADS.BabySleepSounds.Server.Features.Songs.Catalog;

public static class CatalogEndpoint
{
    public static async ValueTask<IResult> GetCatalog(CancellationToken cancellationToken)
    {
        var catalogsResponse = new CatalogResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(catalogsResponse);
    }
}
