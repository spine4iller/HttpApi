using Api.Infra.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api.Infra.Requests;
using TestTask;
using System.Text.Json;

namespace Api.Infra.HttpClients
{
    internal class FirstApiDataProvider : ISearchService
    {
        private readonly FirstApiClient _apiClient;
        private readonly IOptions<FirstApiClientOptions> _options;
        private readonly IMapper _mapper;

        public FirstApiDataProvider(FirstApiClient apiClient, IOptions<FirstApiClientOptions> options,
            IMapper mapper)
        {
            _apiClient = apiClient;
            _options = options;
            _mapper = mapper;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var providerOneSearchRequest = _mapper.Map<ProviderOneSearchRequest>(request);

            using var response = await _apiClient.Client.PostAsJsonAsync(_options.Value.Endpoint,
                providerOneSearchRequest, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<SearchResponse>(stream);
        }

        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
