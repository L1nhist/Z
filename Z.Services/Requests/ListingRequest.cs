namespace Z.Services.Requests;

public class ListingRequest : IRequest
{
    public IEnumerable<string>? Orders { get; set; }
}