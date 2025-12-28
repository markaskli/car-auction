using CarAuction.Api.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace CarAuction.Api.Common.Extensions
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            services.AddEndpoints(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddEndpoints(
            this IServiceCollection services,
            Assembly assembly)
        {
            var serviceDescriptors = assembly
                .DefinedTypes
                .Where(t => !t.IsAbstract && typeof(IFeature).IsAssignableFrom(t))
                .Select(t => ServiceDescriptor.Transient(typeof(IFeature), t))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);
            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            var features = app.Services.GetRequiredService<IEnumerable<IFeature>>();

            foreach (var feature in features)
            {
                feature.MapEndpoints(app);
            }

            return app;
        }
    }
}
