using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Infrastructure.RoleHandling;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var roleClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.Role);
        
        if (roleClaim != null && Enum.TryParse(typeof(Role), roleClaim.Value, out var role))
        {
            if ((Role)role == requirement.RequiredRole)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}