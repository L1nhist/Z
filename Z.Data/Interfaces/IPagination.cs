namespace Z.Data.Interfaces;

public interface IPagination
{
    int PageIndex { get; }

    int PageSize { get; }

    int PageCount { get; }

    int TotalCount { get; }
}