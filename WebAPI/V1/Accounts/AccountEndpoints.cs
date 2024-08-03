using WebAPI.V1.Accounts.Endpoints.Handlers;

namespace WebAPI.V1.Accounts;

internal static class AccountEndpointsMap
{
    public static RouteGroupBuilder MapAccountEndpoints(this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("account/create", AccountEndpoints.CreateAccountHandler);
        return routeGroupBuilder;
    }
}
