using Microsoft.OpenApi.Models;

namespace NET9WithSwagger.Modules.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "Version .Net 9",
                Title = "Swagger UI for .Net 9",
                Description = "Thanks for sharing! Please star and contribute! <a href='https://github.com/aenuhub'>Github</a>",
                TermsOfService = new Uri("https://swagger.io/"),
                Contact = new OpenApiContact
                {
                    Name = "Mehmet Özsığırtmaç - Portfolio",
                    Url = new Uri("https://aenuhub.github.io/personal-website/")
                },
                License = new OpenApiLicense
                {
                    Name = "For testing and reference use",
                    Url = new Uri("https://example.com/license")
                }
            });
            
            // form to generate the swagger documentation
            foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly))
            {
                options.IncludeXmlComments(filePath: name);
            }
            
            // second form to give Authorization via Swagger UI
            var securityScheme = new OpenApiSecurityScheme()
            {
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT" // Optional
            };

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    new string[] { }
                }
            };
            
            options.AddSecurityDefinition("bearerAuth", securityScheme);
            options.AddSecurityRequirement(securityRequirement);
        });
        return services;
    }
}