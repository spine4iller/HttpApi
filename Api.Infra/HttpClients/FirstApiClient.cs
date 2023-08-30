using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Infra.Options;
using Microsoft.Extensions.Options;

namespace Api.Infra.HttpClients
{
    internal class FirstApiClient
    {
        private readonly IOptions<FirstApiClientOptions> _options;
        public HttpClient Client { get; }

        public FirstApiClient(HttpClient client, IOptions<FirstApiClientOptions> options)
        {
            _options = options;
            Client = client;
            Client.BaseAddress = new Uri(_options.Value.BaseAddress);
            Client.Timeout = TimeSpan.FromSeconds(_options.Value.Timeout);
        }
    }
}
