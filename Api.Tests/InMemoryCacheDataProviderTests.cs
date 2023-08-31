using Api.Infra.DataProviders;
using Api.Infra.Interfaces;
using FluentAssertions;

namespace Api.Tests
{
    public class InMemoryCacheDataProviderTests
    {
        [Theory, AutoMoqData]
        public void CacheRoutes_WhenNewRoutes_ThenAdd(
            InMemoryCacheDataProvider dataProvider,
            List<Route> routes)
        {
            int added = dataProvider.CacheRoutes(routes);

            added.Should().Be(routes.Count);
        }

        [Theory, AutoMoqData]
        public void CacheRoutes_WhenRoutesExist_ThenSkipAdd(
            InMemoryCacheDataProvider dataProvider,
            List<Route> routes)
        {
            dataProvider.CacheRoutes(routes);

            int added = dataProvider.CacheRoutes(routes);

            added.Should().Be(0);
        }

        [Theory, AutoMoqData]
        public void CacheRoutes_WhenJustAnotherRouteId_ThenSkipAdd(
            InMemoryCacheDataProvider dataProvider,
            List<Route> routes)
        {
            dataProvider.CacheRoutes(routes);
            routes.First().Id = Guid.NewGuid();

            int added = dataProvider.CacheRoutes(routes);

            added.Should().Be(0);
        }
    }
}