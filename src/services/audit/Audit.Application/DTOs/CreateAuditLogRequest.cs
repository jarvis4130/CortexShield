namespace Audit.Application.DTOs;

public record CreateAuditLogRequest(
    string EntityId,
    string EntityType,
    string Action,
    string PerformedBy,
    string? Data = null
);