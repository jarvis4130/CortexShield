namespace Audit.Domain.Entities;

public class AuditLog
{
    public string id { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Data { get; set; } = string.Empty;
}