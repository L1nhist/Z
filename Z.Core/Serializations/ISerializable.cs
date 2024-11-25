namespace Z.Core.Serializations;

public interface ISerializer<TSrc, TDes>
{
    TSrc? Deserialize(TDes destination);

    TDes? Serialize(TSrc source);
}