using Api.Infra.DataProviders;
using Api.Infra.Interfaces;

namespace Api.Infra.Services
{
    public class SearchService : ISearchService
    {
        private readonly FirstApiDataProvider _firstApiDataProvider;
        private readonly SecondApiDataProvider _secondApiDataProvider;
        private readonly ICacheDataProvider _cache;

        public SearchService(FirstApiDataProvider firstApiDataProvider,
            SecondApiDataProvider secondApiDataProvider, ICacheDataProvider cache)
        {
            _firstApiDataProvider = firstApiDataProvider;
            _secondApiDataProvider = secondApiDataProvider;
            _cache = cache;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            List<Route> foundRoutes = new();
            var cached = _cache.Search(request, cancellationToken);
            foundRoutes.AddRange(cached);
            if (request.Filters?.OnlyCached is null || !request.Filters.OnlyCached.Value)
            {
                var results = await Task.WhenAll(
                    _firstApiDataProvider.SearchAsync(request, cancellationToken),
                    _secondApiDataProvider.SearchAsync(request, cancellationToken)
                );
                var externalRoutes = results.SelectMany(r => r).ToList();
                _cache.CacheRoutes(externalRoutes);
                foundRoutes.AddRange(externalRoutes);
            }

            if (request.Filters is not null)
            {
                if (request.Filters.MinTimeLimit.HasValue)
                    foundRoutes = foundRoutes.Where(r => r.TimeLimit >= request.Filters.MinTimeLimit.Value).ToList();
                if (request.Filters.MaxPrice.HasValue)
                    foundRoutes = foundRoutes.Where(r => r.Price <= request.Filters.MaxPrice.Value).ToList();
            }

            var allRoutes = foundRoutes.ToArray();
            if (!allRoutes.Any())
            {
                return new SearchResponse();
            }

            return new SearchResponse()
            {
                Routes = allRoutes,
                MaxMinutesRoute = allRoutes.Max(m => (m.DestinationDateTime - m.OriginDateTime).Minutes),
                MinMinutesRoute = allRoutes.Min(m => (m.DestinationDateTime - m.OriginDateTime).Minutes),
                MaxPrice = allRoutes.Max(p => p.Price),
                MinPrice = allRoutes.Min(p => p.Price),
            };
        }

        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}