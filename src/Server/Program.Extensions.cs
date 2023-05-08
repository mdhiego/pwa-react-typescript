using System.Reflection;
using System.Text;
using System.Text.Json;
using BabySounds.Server.Brokers.Emailing;
using BabySounds.Server.Brokers.Persistence;
using BabySounds.Server.Brokers.SystemClock;
using BabySounds.Server.Configuration;
using BabySounds.Server.Helpers;
using BabySounds.Server.Services.JwtGeneration;
using BabySounds.Server.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BabySounds.Server;

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
            .Configure<JsonOptions>(static options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .Configure<RequestLocalizationOptions>(static options =>
            {
                options.SupportedCultures = Constants.SupportedCultures;
                options.SupportedUICultures = Constants.SupportedCultures;
                options.DefaultRequestCulture = new RequestCulture(Constants.DefaultCulture);
            });

        return services;
    }

    internal static IServiceCollection AddBrokers(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddSingleton<ISystemClock, SystemClock>();

        services
            .AddScoped<ApplicationDbContext>()
            .AddDbContext<ApplicationDbContext>()
            .AddHostedService<ApplicationDbSetup>();

        if (environment.IsDevelopment()) services.AddScoped<IEmailSender, FakeEmailSender>();
        else services.AddScoped<IEmailSender, SmtpEmailSender>();

        return services;
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

        services
            .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtBearerSettings>>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Value.Issuer,
                    ValidAudience = jwtOptions.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                };
            }).Services
            .AddAuthorization();

        services
            .AddHttpContextAccessor()
            .AddCors(options =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var corsOptions = scope.ServiceProvider.GetRequiredService<IOptions<CorsSettings>>();

                options.AddPolicy(Constants.CorsPolicy, builder => builder
                    .WithOrigins(corsOptions.Value.AllowedOrigins.Split(";"))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            })
            .AddLocalization()
            .AddHealthChecks().Services
            .AddProblemDetails()
            .AddRouting(static options => options.LowercaseUrls = true)
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}
