using MediatR;
using Audit.Application.DTOs;

namespace Audit.Application.Queries.GetAuditByEntityId;

public record GetAuditByEntityIdQuery(string EntityId) : IRequest<GetAuditLogsResponse>;