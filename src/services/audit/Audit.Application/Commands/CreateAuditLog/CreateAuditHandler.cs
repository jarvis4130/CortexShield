using MediatR;
using Audit.Application.DTOs;
using Audit.Domain.Entities;
using Audit.Domain.Repositories;

namespace Audit.Application.Commands.CreateAuditLog;

public class CreateAuditHandler : IRequestHandler<CreateAuditCommand, CreateAuditLogResponse>
{
    private readonly IAuditRepository _auditRepository;

    public CreateAuditHandler(IAuditRepository auditRepository)
    {
        _auditRepository = auditRepository;
    }

    public async Task<CreateAuditLogResponse> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
    {
        var auditId = Guid.NewGuid().ToString();
        
        var auditLog = new AuditLog
        {
            id = auditId,
            Id = auditId,
            EntityId = request.EntityId,
            EntityType = request.EntityType,
            Action = request.Action,
            PerformedBy = request.PerformedBy,
            Timestamp = DateTime.UtcNow,
            Data = request.Data ?? string.Empty
        };

        await _auditRepository.SaveAsync(auditLog);

        return new CreateAuditLogResponse(auditId, "Logged");
    }
}