using MediatR;
using Audit.Application.DTOs;

namespace Audit.Application.Commands.CreateAuditLog;

public record CreateAuditCommand(
    string EntityId,
    string EntityType,
    string Action,
    string PerformedBy,
    string? Data = null
) : IRequest<CreateAuditLogResponse>;