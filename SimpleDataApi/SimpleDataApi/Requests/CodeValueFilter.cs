namespace SimpleDataApi.Requests
{
    public class CodeValueFilter : PagedRequest
    {
        public int? Code { get; set; }
        public string? ValuePrefix { get; set; }
    }
}
