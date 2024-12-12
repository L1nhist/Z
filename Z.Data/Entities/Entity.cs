﻿namespace Z.Data.Entities;

public class Entity<T> : IEntity<T>
{
    object[] IEntity.Keys => [Id!];

    [Key]
    [Required]
    [NotNull]
    public required T Id { get; set; }
}

public class Entity : Entity<Uuid>
{ }