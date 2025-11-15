using MediatR;
using Upload.Application.DTOs;

namespace Upload.Application.Commands.InitUpload;

public class InitUploadCommand : IRequest<InitUploadResponse>
{
    public string Type { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long ContentLength { get; set; }
    public string ContentHash { get; set; } = string.Empty;
}