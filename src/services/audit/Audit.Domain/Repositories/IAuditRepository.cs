using Audit.Domain.Entities;

namespace Audit.Domain.Repositories;

public interface IAuditRepository
{
    Task SaveAsync(AuditLog log);
    Task<List<AuditLog>> GetByEntityIdAsync(string entityId);
}