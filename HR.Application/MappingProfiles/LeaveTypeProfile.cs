﻿using AutoMapper;
using HR.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.Application.Features.LeaveType.Queries.GetAllLeaveType;
using HR.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.Domain;

namespace HR.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
	public LeaveTypeProfile()
	{
		CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();

		CreateMap<LeaveType, LeaveTypeDetailsDto>();

		CreateMap<CreateLeaveTypeCommand, LeaveType>();
		CreateMap<UpdateLeaveTypeCommand, LeaveType>();
	}
}
