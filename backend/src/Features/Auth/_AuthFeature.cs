using Asp.Versioning;
using CarAuction.Api.Common.Interfaces;

namespace CarAuction.Api.Features.Auth
{
    public class _AuthFeature : IFeature
    {
        public const string RoutePrefix = "api/v{version:apiVersion}/auth";
        public const string Tag = "Auth";

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi();

            var group = versionedApi
                .MapGroup(RoutePrefix)
                .WithTags(Tag)
                .HasApiVersion(new ApiVersion(1, 0))
                .RequireAuthorization();

            group.MapGetCurrentUserEndpoint();
        }
    }
}
