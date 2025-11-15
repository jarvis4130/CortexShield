using MediatR;
using Content.Application.DTOs;

namespace Content.Application.Queries.GetContent;

public record GetContentQuery(string ContentId) : IRequest<ContentResponse>;