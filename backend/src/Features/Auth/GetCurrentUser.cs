using System.Security.Claims;

namespace CarAuction.Api.Features.Auth
{
    public static class GetCurrentUserEndpoint
    {
        public static void MapGetCurrentUserEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/me", Handle)
                .WithName("GetCurrentUser");
        }
        private static async Task<IResult> Handle(ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);

            return Results.Ok(result);
        }
    }
}
