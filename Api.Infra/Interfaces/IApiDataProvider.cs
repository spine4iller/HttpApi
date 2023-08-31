namespace Api.Infra.Interfaces
{
    public interface IApiDataProvider
    {
        Task<List<Route>> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    }
}