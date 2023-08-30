using TestTask;

namespace Api.Infra.Services
{
    public class SearchService : ISearchService
    {
        public SearchService(ihttp)
        {

        }
        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}