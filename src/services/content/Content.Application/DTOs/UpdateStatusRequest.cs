namespace Content.Application.DTOs;

public record UpdateStatusRequest(
    string Status,
    double CombinedScore,
    double ConfidenceScore,
    string Priority,
    string? ReviewQueueId = null
);