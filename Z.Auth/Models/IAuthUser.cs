using System.Security.Principal;

namespace Z.Auth.Models;

public interface IAuthUser : IPrincipal
{
    string? Username { get; }

    string? Fullname { get; }

    IEnumerable<string>? Roles { get; }

    IEnumerable<string>? Policies { get; }
}