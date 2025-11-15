using MediatR;
using Upload.Application.DTOs;

namespace Upload.Application.Queries.GetContent;

public class GetContentQuery : IRequest<GetContentResponse>
{
    public string ContentId { get; set; } = string.Empty;
}