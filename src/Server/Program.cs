using BabySounds.Server;
using BabySounds.Server.Features.Auth;
using BabySounds.Server.Features.Categories;
using BabySounds.Server.Features.Player;
using BabySounds.Server.Features.Playlists;
using BabySounds.Server.Features.Search;
using BabySounds.Server.Features.Tracks;
using BabySounds.Server.Features.Users;
using BabySounds.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddOptions(builder.Configuration)
        .ConfigureOptions()
        .AddBrokers(builder.Environment)
        .AddServices();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app
            .UseDeveloperExceptionPage()
            .UseMigrationsEndPoint()
            .UseWebAssemblyDebugging();
    }
    else
    {
        // The default HSTS value is 30 days.
        app.UseHsts();
    }

    if (!app.Environment.IsProduction())
    {
        app
            .UseDefaultFiles()
            .UseSwagger()
            .UseSwaggerUI(static options =>
            {
                options.DefaultModelsExpandDepth(0);
                options.DisplayRequestDuration();
            });
    }

    app
        .UseStaticFiles()
        .UseBlazorFrameworkFiles();

    app
        .UseHttpsRedirection()
        .UseCors(Constants.CorsPolicy)
        .UseRequestLocalization()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization();

    app
        .MapAuthEndpoints()
        .MapUsersEndpoints()
        .MapTracksEndpoints()
        .MapSearchEndpoints()
        .MapCategoriesEndpoints()
        .MapPlaylistsEndpoints()
        .MapPlayerEndpoints()
        .MapHealthChecks("/health");
}

app.Run();
