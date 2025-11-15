namespace Audit.Application.DTOs;

public record CreateAuditLogResponse(
    string Id,
    string Status
);