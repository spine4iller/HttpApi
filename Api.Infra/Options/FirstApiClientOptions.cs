namespace Api.Infra.Options
{
    public class FirstApiClientOptions
    {
        public string BaseAddress { get; set; }
        public string Endpoint { get; set; }
        public int Timeout { get; set; } = 10;
    }
}