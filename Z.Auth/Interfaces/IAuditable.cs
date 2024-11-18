namespace Z.Data.Interfaces;

public interface IAuditable : IMomentable
{
    string Creator { get; set; }

    string Modifier { get; set; }
}