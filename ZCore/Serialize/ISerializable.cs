﻿namespace Z.Core.Serialize;

public interface ISerializer<TSrc, TDes>
{
    TSrc? Deserialize(TDes destination);

    TDes? Serialize(TSrc source);
}