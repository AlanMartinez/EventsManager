using EventService.Application.Commands;
using EventService.Application.Commands.Handlers;
using EventService.Exceptions;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;
using Moq;

namespace EventService.Test.Application.Commands
{
    public class RegisterEventAttendeeCommandHandlerTest
    {
        [Fact]
        public async Task RegisterAttendeeToEvent_ShouldReturnOk_Ok()
        {
            var sut = GetSut(out var _mockEventRepository,
                             out var _mockAttendeeRepository,
                             out var _mockEventAttendeeRepository);

            _mockEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Event { });
            _mockAttendeeRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Attendee());
            _mockEventAttendeeRepository.Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var command = new RegisterEventAttendeeCommand(new Guid(), new Guid());

            var result = await sut.Handle(command, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task RegisterAttendeeToEvent_ShouldReturnException_AttendeeAlreadyRegistered()
        {
            var sut = GetSut(out var _mockEventRepository,
                             out var _mockAttendeeRepository,
                             out var _mockEventAttendeeRepository);

            _mockEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Event { });
            _mockAttendeeRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Attendee());
            _mockEventAttendeeRepository.Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(new EventAttendee());

            var command = new RegisterEventAttendeeCommand(new Guid(), new Guid());

            Assert.ThrowsAsync<AttendeeAlreadyRegistered>(async () => await sut.Handle(command, CancellationToken.None));

            
        }

        [Fact]
        public async Task RegisterAttendeeToEvent_ShouldReturnException_EventNotFoundException()
        {
            var sut = GetSut(out var _mockEventRepository,
                             out var _,
                             out var _);

            _mockEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

            var command = new RegisterEventAttendeeCommand(new Guid(), new Guid());

            Assert.ThrowsAsync<EventNotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task RegisterAttendeeToEvent_ShouldReturnException_AttendeeNotFoundException()
        {
            var sut = GetSut(out var _mockEventRepository,
                             out var _mockAttendeeRepository,
                             out var _);
            _mockEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Event { });
            _mockAttendeeRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

            var command = new RegisterEventAttendeeCommand(new Guid(), new Guid());

            Assert.ThrowsAsync<AttendeeNotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        private RegisterEventAttendeeCommandHandler GetSut(out Mock<IEventRepository> eventRepositoryMock,
                                                    out Mock<IGenericRepository<Attendee>> attendeeRepositoryMock,
                                                    out Mock<IGenericRepository<EventAttendee>> eventAttendeeRepositoryMock)
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            attendeeRepositoryMock = new Mock<IGenericRepository<Attendee>>();
            eventAttendeeRepositoryMock = new Mock<IGenericRepository<EventAttendee>>();
            
            return new RegisterEventAttendeeCommandHandler(eventRepositoryMock.Object, attendeeRepositoryMock.Object, eventAttendeeRepositoryMock.Object);
        }
    }
}