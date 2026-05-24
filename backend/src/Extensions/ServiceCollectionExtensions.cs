using Microsoft.OpenApi;

namespace CarAuction.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerGenWithAuthSupport(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                            TokenUrl = new Uri(configuration["Keycloak:TokenUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid" },
                                { "profile", "profile" },
                            }
                        }
                    }
                });

                o.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Keycloak", doc),
                        []
                    }
                });
            });

            return services;
        }
    }
}
