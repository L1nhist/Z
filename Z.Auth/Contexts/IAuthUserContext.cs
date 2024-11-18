using Z.Auth.Models;

namespace Z.Auth.Contexts;

public interface IAuthUserContext
{
    bool IsAuthenticated { get; }

    IAuthUser? User { get; }
}