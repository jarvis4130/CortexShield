using MediatR;
using Upload.Application.DTOs;

namespace Upload.Application.Commands.CompleteUpload;

public class CompleteUploadCommand : IRequest<CompleteUploadResponse>
{
    public string ContentId { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
}