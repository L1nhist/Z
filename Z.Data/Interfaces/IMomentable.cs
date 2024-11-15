using Z.Core.StrongTypes;

namespace Z.Data.Interfaces;

public interface IMomentable
{
    Epoch CreatedAt { get; set; }

    Epoch ModifiedAt { get; set; }

    Epoch RemovedAt { get; set; }
}