using MediatR;
using Microsoft.AspNetCore.Mvc;
using Audit.Application.DTOs;
using Audit.Application.Commands.CreateAuditLog;
using Audit.Application.Queries.GetAuditByEntityId;

namespace Audit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuditController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("log")]
    public async Task<ActionResult<CreateAuditLogResponse>> CreateAuditLog([FromBody] CreateAuditLogRequest request)
    {
        var command = new CreateAuditCommand(
            request.EntityId,
            request.EntityType,
            request.Action,
            request.PerformedBy,
            request.Data
        );

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{entityId}")]
    public async Task<ActionResult<GetAuditLogsResponse>> GetAuditLogs(string entityId)
    {
        var query = new GetAuditByEntityIdQuery(entityId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}