using Api.Infra.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using Api.Infra.Requests;
using System.Text.Json;
using Api.Infra.Interfaces;
using Api.Infra.HttpClients;

namespace Api.Infra.DataProviders
{
    public class SecondApiDataProvider : IApiDataProvider
    {
        private readonly SecondApiClient _apiClient;
        private readonly IOptions<SecondApiClientOptions> _options;
        private readonly IMapper _mapper;

        public SecondApiDataProvider(SecondApiClient apiClient, IOptions<SecondApiClientOptions> options,
            IMapper mapper)
        {
            _apiClient = apiClient;
            _options = options;
            _mapper = mapper;
        }

        public async Task<List<Route>> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            if (!await IsAvailableAsync(cancellationToken))
            {
                return new List<Route>();
            }
            var searchRequest = _mapper.Map<ProviderTwoSearchRequest>(request);
            if (request.Filters is not null)
            {
                searchRequest.MinTimeLimit = request.Filters.DestinationDateTime;
            }
            using var response = await _apiClient.Client.PostAsJsonAsync(_options.Value.SearchEndpoint,
                searchRequest, cancellationToken);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var searchResponse = await JsonSerializer.DeserializeAsync<ProviderTwoSearchResponse>(stream);
            return searchResponse!.Routes.Select(route => _mapper.Map<Route>(route)).ToList();
        }

        async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            using var response = await _apiClient.Client.GetAsync(_options.Value.PingEndpoint,
                cancellationToken);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
