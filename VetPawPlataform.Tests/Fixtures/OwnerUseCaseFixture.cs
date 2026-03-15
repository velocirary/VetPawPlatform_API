using Moq;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Tests.Fixtures;

public class OwnerUseCaseFixture
{
    public Mock<IOwnerRepository> RepositoryMock { get; }

    public OwnerUseCaseFixture()
    {
        RepositoryMock = new Mock<IOwnerRepository>();
    }
}