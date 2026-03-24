using Moq;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Tests.Fixtures;

public class AppointmentUseCaseFixture
{
    public Mock<IAppointmentRepository> RepositoryMock { get; }

    public AppointmentUseCaseFixture()
    {
        RepositoryMock = new Mock<IAppointmentRepository>();
    }
}
