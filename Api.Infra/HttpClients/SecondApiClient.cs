using Api.Infra.Options;
using Microsoft.Extensions.Options;

namespace Api.Infra.HttpClients
{
    public class SecondApiClient
    {
        public HttpClient Client { get; }
        public SecondApiClient(HttpClient client, IOptions<SecondApiClientOptions> options)
        {
            Client = client;
            Client.BaseAddress = new Uri(options.Value.BaseAddress!);
            Client.Timeout = TimeSpan.FromSeconds(options.Value.Timeout);
        }
    }
}
