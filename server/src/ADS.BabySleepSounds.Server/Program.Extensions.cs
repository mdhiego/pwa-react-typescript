using System.Reflection;
using System.Text;
using System.Text.Json;
using ADS.BabySleepSounds.Server.Configuration;
using ADS.BabySleepSounds.Server.Helpers;
using ADS.BabySleepSounds.Server.Infrastructure.Persistence;
using ADS.BabySleepSounds.Server.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ADS.BabySleepSounds.Server;

internal static class ProgramExtensions
{
    internal static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddOptions<DbSettings>()
            .Bind(configuration.GetSection(DbSettings.Key));
        services
            .AddOptions<JwtBearerSettings>()
            .Bind(configuration.GetSection(JwtBearerSettings.Key));
        services
            .AddOptions<CorsSettings>()
            .Bind(configuration.GetSection(CorsSettings.Key));

        return services;
    }

    internal static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        services
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedCultures = Constants.SupportedCultures;
                options.SupportedUICultures = Constants.SupportedCultures;
                options.DefaultRequestCulture = new RequestCulture(Constants.DefaultCulture);
            });

        return services;
    }

    internal static IServiceCollection AddApplicationServices(this IServiceCollection services
    )
    {
        services.AddValidatorsFromAssemblies(new[]
        {
            Assembly.GetExecutingAssembly()
        });

        return services;
    }

    internal static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (environment.IsDevelopment())
            {
                var dbContextOptionsBuilder = options.UseSqlite();
                dbContextOptionsBuilder
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
            else
            {
                // options.UseSqlServer(dbSettings.DefaultConnection);
            }
        });
        // Register the worker responsible of seeding the database with the sample clients.
        services.AddHostedService<ApplicationDbSetup>();

        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtBearerSettings>>();

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Value.Issuer,
                    ValidAudience = jwtOptions.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey!))
                };
            });

        if (environment.IsDevelopment()) services.AddScoped<IEmailSender, FakeEmailSender>();
        else services.AddScoped<IEmailSender, SmtpEmailSender>();

        return services;
    }

    internal static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var corsOptions = scope.ServiceProvider.GetRequiredService<IOptions<CorsSettings>>();

        services
            .AddHttpContextAccessor()
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicy, builder =>
                {
                    builder
                        .WithOrigins(corsOptions.Value.AllowedOrigins.Split(";"))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            })
            .AddLocalization()
            .AddProblemDetails()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}
