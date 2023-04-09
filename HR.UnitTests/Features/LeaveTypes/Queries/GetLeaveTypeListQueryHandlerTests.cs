using AutoMapper;
using HR.Application.Contracts.Logger;
using HR.Application.Contracts.Persistence;
using HR.Application.Features.LeaveType.Queries.GetAllLeaveType;
using HR.Application.MappingProfiles;
using HR.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{

    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypeQueryHandler>> _mockAppLogger;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypeQueryHandler>>();

    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypeQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypeQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
