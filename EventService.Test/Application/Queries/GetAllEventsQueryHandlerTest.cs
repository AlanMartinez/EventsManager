using EventService.Application.Queries;
using EventService.Application.Queries.Handlers;
using EventService.Infrastructure;
using EventService.Models;
using EventService.Services;
using Moq;

namespace EventService.Test.Application.Queries
{
    public class GetAllEventsQueryHandlerTest
    {
        [Fact]
        public async Task GetAllEvents_ShouldReturnEvents_Empty()
        {
            var mockRepositoryRepository = new Mock<IEventRepository>();
            mockRepositoryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Event> { });

            var mockRepositoryCache = new Mock<ICacheService<Event>>();
            mockRepositoryCache.Setup(x => x.GetCachedEventsAsync());

            var handler = new GetAllEventsQueryHandler(mockRepositoryRepository.Object, mockRepositoryCache.Object);
            var query = new GetAllEventsQuery();
            
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Empty(result);
        }
    }
}