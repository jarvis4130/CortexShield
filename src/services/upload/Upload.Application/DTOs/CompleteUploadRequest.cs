namespace Upload.Application.DTOs;

public class CompleteUploadRequest
{
    public string ContentId { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
}