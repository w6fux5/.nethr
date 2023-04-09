using HR.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController : ControllerBase
{
    private readonly IMediator _mediatior;

    public LeaveAllocationController(IMediator mediatior)
    {
        _mediatior = mediatior;
    }
    // GET: api/<LeaveAllocationController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediatior.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }

    // GET api/<LeaveAllocationController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var leaveAllocation = await _mediatior.Send(new LeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }

    // POST api/<LeaveAllocationController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediatior.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediatior.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediatior.Send(command);
        return NoContent();
    }
}
