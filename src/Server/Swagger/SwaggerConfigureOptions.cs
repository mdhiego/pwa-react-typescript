using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BabySounds.Server.Swagger;

/// <inheritdoc />
/// <summary>
/// Configures the Swagger generation options.
/// </summary>
/// <remarks>This allows API versioning to define a Swagger document per API version after the
/// <see cref="T:Asp.Versioning.ApiExplorer.IApiVersionDescriptionProvider" /> service has been resolved from the service container.</remarks>
public sealed class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        var info = new OpenApiInfo
        {
            Title = "BabySleep API",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "BabySleep",
                Email = "contact@babysleep.com",
            }
        };

        options.SwaggerDoc("v1", info);

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        };
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

        var securityReq = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };
        options.AddSecurityRequirement(securityReq);
    }
}
