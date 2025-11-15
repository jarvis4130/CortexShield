namespace Audit.Application.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(string entityId, string entityType, string action, string performedBy, object? data = null);
}