namespace Z.Core.Models;

public interface IMomentable
{
    Epoch CreatedAt { get; set; }

    Epoch ModifiedAt { get; set; }
}