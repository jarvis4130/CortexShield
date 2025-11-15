using MediatR;
using Audit.Application.DTOs;
using Audit.Domain.Repositories;

namespace Audit.Application.Queries.GetAuditByEntityId;

public class GetAuditByEntityIdHandler : IRequestHandler<GetAuditByEntityIdQuery, GetAuditLogsResponse>
{
    private readonly IAuditRepository _auditRepository;

    public GetAuditByEntityIdHandler(IAuditRepository auditRepository)
    {
        _auditRepository = auditRepository;
    }

    public async Task<GetAuditLogsResponse> Handle(GetAuditByEntityIdQuery request, CancellationToken cancellationToken)
    {
        var auditLogs = await _auditRepository.GetByEntityIdAsync(request.EntityId);

        var logDtos = auditLogs.Select(log => new AuditLogDto(
            log.Id,
            log.Action,
            log.PerformedBy,
            log.Timestamp,
            string.IsNullOrEmpty(log.Data) ? null : log.Data
        )).ToList();

        return new GetAuditLogsResponse(request.EntityId, logDtos);
    }
}