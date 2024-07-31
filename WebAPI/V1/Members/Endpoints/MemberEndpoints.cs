using WebAPI.V1.Members.Endpoints.Handlers;

namespace WebAPI.V1.Members.Endpoints;

internal static class MemberEndpointsMap
{
    public static RouteGroupBuilder MapMemberEndpoints(this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("member/register", MemberEndpoints.CreateMemberHandler);
        routeGroupBuilder.MapPost("member/validate", MemberEndpoints.ValidateMemberHandler);
        return routeGroupBuilder;
    }
}
