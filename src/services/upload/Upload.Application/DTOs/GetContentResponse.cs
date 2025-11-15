namespace Upload.Application.DTOs;

public class GetContentResponse
{
    public string ContentId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ContentHash { get; set; } = string.Empty;
    public DateTime? UploadedAt { get; set; }
}