﻿using Api.Infra.Options;
using Microsoft.Extensions.Options;

namespace Api.Infra.HttpClients
{
    public class FirstApiClient
    {
        public HttpClient Client { get; }
        public FirstApiClient(HttpClient client, IOptions<FirstApiClientOptions> options)
        {
            Client = client;
            Client.BaseAddress = new Uri(options.Value.BaseAddress!);
            Client.Timeout = TimeSpan.FromSeconds(options.Value.Timeout);
        }
    }
}
