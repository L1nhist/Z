﻿namespace Z.Core.Models;

public interface IMomentable
{
    long? CreatedAt { get; set; }

    long? ModifiedAt { get; set; }
}