namespace Upload.Domain.Entities;

public class Content
{
    public string id { get; set; } = string.Empty;
    public string contentId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentHash { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UploadedAt { get; set; }
}