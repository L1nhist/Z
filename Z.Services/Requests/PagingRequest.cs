namespace Z.Services.Requests;

public class PagingRequest : IRequest
{
    public int? PageIndex { get; set; }

    public int? PageSize { get; set; }
}