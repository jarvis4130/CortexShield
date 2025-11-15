namespace Audit.Application.DTOs;

public record GetAuditLogsResponse(
    string EntityId,
    List<AuditLogDto> Logs
);

public record AuditLogDto(
    string Id,
    string Action,
    string PerformedBy,
    DateTime Timestamp,
    string? Data
);