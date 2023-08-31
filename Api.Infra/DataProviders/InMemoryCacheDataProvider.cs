using Api.Infra.Interfaces;

namespace Api.Infra.DataProviders
{
    public class InMemoryCacheDataProvider : ICacheDataProvider
    {
        private static readonly List<Route> Routes = new();

        public List<Route> Search(SearchRequest request, CancellationToken token)
        {
            var routes = Routes.Where(r => request.Origin == r.Origin
                                            && request.Destination == r.Destination
                                            && request.OriginDateTime == r.OriginDateTime).ToList();
            if (request.Filters is null)
                return routes;

            var filters = request.Filters;
            routes = routes.Where(r => (filters.MaxPrice is null || filters.MaxPrice.Value >= r.Price) &&
                                       (filters.DestinationDateTime is null || filters.DestinationDateTime.Value == r.DestinationDateTime)
                                       && (filters.MinTimeLimit is null || filters.MinTimeLimit.Value <= r.TimeLimit)).ToList();

            return routes;
        }

        public int CacheRoutes(List<Route> routes)
        {
            var newRoutes = routes.Except(Routes).ToList();
            Routes.AddRange(newRoutes);
            return newRoutes.Count;
        }
    }
}
