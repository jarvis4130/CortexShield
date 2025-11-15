using MediatR;
using Content.Application.DTOs;

namespace Content.Application.Commands.UpdateContentStatus;

public record UpdateContentStatusCommand(
    string ContentId,
    string Status,
    double CombinedScore,
    double ConfidenceScore,
    string Priority,
    string? ReviewQueueId = null
) : IRequest<UpdatedResponse>;