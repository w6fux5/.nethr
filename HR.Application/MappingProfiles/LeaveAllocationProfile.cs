using AutoMapper;
using HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.Domain;

namespace HR.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{

	public LeaveAllocationProfile()
	{
		CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
		CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
		CreateMap<CreateLeaveTypeCommand, LeaveAllocation>();
		CreateMap<UpdateLeaveTypeCommand, LeaveAllocation>();
	}
}
