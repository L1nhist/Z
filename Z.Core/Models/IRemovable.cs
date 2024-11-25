namespace Z.Core.Models;

public interface IRemovable
{
    bool IsDeleted { get; set; }
}