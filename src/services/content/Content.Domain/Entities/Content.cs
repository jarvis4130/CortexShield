namespace Content.Domain.Entities;

public class Content
{
    public string id { get; set; } = string.Empty;
    public string contentId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string FinalStatus { get; set; } = string.Empty;
    public double CombinedScore { get; set; }
    public double ConfidenceScore { get; set; }
    public string Priority { get; set; } = string.Empty;
    public string? ReviewQueueId { get; set; }
    public DateTime? DeletedAt { get; set; }
}