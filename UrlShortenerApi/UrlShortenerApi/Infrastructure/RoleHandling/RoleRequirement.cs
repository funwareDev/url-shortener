using Microsoft.AspNetCore.Authorization;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Infrastructure.RoleHandling;

public class RoleRequirement : IAuthorizationRequirement
{
    public Role RequiredRole { get; }

    public RoleRequirement(Role requiredRole)
    {
        RequiredRole = requiredRole;
    }
}