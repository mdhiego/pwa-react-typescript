using ADS.BabySleepSounds.Server;
using ADS.BabySleepSounds.Server.Configuration;
using ADS.BabySleepSounds.Server.Features.Auth;
using ADS.BabySleepSounds.Server.Features.User;
using ADS.BabySleepSounds.Server.Helpers;



var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddOptions(builder.Configuration)
        .ConfigureOptions()
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Environment)
        .AddWebApiServices();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app
            .UseDeveloperExceptionPage()
            .UseDefaultFiles()
            .UseStaticFiles()
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(0);
                options.DisplayRequestDuration();
            });
    }
    else app.UseHsts();

    app
        .UseCors(Constants.CorsPolicy)
        .UseRequestLocalization()
        .UseRouting()
        .UseAuthentication();

    app
        .MapAuthEndpoints()
        .MapUserEndpoints();
}

app.Run();
