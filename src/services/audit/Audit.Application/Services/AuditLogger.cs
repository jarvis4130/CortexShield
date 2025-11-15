using MediatR;
using System.Text.Json;
using Audit.Application.Interfaces;
using Audit.Application.Commands.CreateAuditLog;

namespace Audit.Application.Services;

public class AuditLogger : IAuditLogger
{
    private readonly IMediator _mediator;

    public AuditLogger(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task LogAsync(string entityId, string entityType, string action, string performedBy, object? data = null)
    {
        var dataJson = data != null ? JsonSerializer.Serialize(data) : null;
        
        var command = new CreateAuditCommand(
            entityId,
            entityType,
            action,
            performedBy,
            dataJson
        );

        await _mediator.Send(command);
    }
}