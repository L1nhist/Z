using System.ComponentModel.DataAnnotations;
using Z.Core.StrongTypes;

namespace Z.Data.Entities;

public class Entity<T> : IEntity<T>
{
    object[] IEntity.Keys => [Id!];

    [Key]
    public required T Id { get; set; }
}

public class Entity : Entity<Uuid>
{ }