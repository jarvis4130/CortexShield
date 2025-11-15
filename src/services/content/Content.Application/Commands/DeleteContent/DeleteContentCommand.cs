using MediatR;
using Content.Application.DTOs;

namespace Content.Application.Commands.DeleteContent;

public record DeleteContentCommand(string ContentId) : IRequest<SuccessResponse>;