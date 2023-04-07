using AutoMapper;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveType.Commands.CreateLeaveType;

internal class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly CreateLeaveTypeCommandValidator _valiator;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, CreateLeaveTypeCommandValidator valiator)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _valiator = valiator;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Valid incoming data
        var validationResult = await _valiator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }


        // convert to domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // return record id
        return leaveTypeToCreate.Id;
    }
}
