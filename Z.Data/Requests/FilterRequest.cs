namespace Z.Data.Requests;

public class FilterRequest : IRequest
{
    public string? FilterBy { get; set; }

    public IEnumerable<string>? OrderBy { get; set; }
}