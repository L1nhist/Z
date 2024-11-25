namespace Z.Core.Models;

public interface IAuditable : IMomentable
{
    string Creator { get; set; }

    string Modifier { get; set; }
}