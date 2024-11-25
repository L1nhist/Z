﻿namespace Z.Core.Models;

public interface IPagination
{
    int PageIndex { get; }

    int PageSize { get; }

    int PageCount { get; }

    int TotalCount { get; }
}