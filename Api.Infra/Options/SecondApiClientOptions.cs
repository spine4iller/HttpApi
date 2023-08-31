namespace Api.Infra.Options
{
    public class SecondApiClientOptions
    {
        public string? BaseAddress { get; set; }
        public int Timeout { get; set; } = 3;
        public string? SearchEndpoint { get; set; }
        public string? PingEndpoint { get; set; }
    }
}