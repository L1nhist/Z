using System.Security.Principal;

namespace Z.Core.Authentications;

public interface IAuthUser : IPrincipal
{
    string? Email { get; }

    string? Username { get; }

    IEnumerable<string>? Roles { get; }

    IEnumerable<string>? Policies { get; }
}