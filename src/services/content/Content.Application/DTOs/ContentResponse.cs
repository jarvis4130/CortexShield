namespace Content.Application.DTOs;

public record ContentResponse(
    string ContentId,
    string FinalStatus,
    double CombinedScore,
    double ConfidenceScore,
    string Priority,
    string? ReviewQueueId
);