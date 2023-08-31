namespace Api.Infra.Interfaces
{
    public interface ICacheDataProvider
    {
        List<Route> Search(SearchRequest request, CancellationToken cancellationToken);
        int CacheRoutes(List<Route> routes);
    }
}
