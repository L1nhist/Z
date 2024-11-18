using System.Security.Claims;
using Z.Core.StrongTypes;

namespace Z.Auth.Models;

public class AuthUser : ClaimsPrincipal, IAuthUser
{
    public Uuid Id { get; }

    public string? Username { get; }

    public string? Fullname { get; }

    public IEnumerable<string>? Roles { get; }

    public IEnumerable<string>? Policies { get; }

    public AuthUser(ClaimsPrincipal principal) : base(principal)
    {
        Id = new(Get("id"));
        Username = Get("Email");
        Fullname = Get("Name", "Sub");
        Roles = Get("Role")?.Split(',', StringSplitOptions.RemoveEmptyEntries);
        Policies = Get("Policy")?.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }

    public string? Get(params string[] claims)
        => FindFirst(c => claims.Contains(c.Type))?.Value;
}