using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Upload.Application.Interfaces;

namespace Upload.Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> GenerateUploadUrlAsync(string fileName, string contentId)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("uploads");
        await containerClient.CreateIfNotExistsAsync();
        
        var blobName = $"{contentId}_{fileName}";
        var blobClient = containerClient.GetBlobClient(blobName);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = "uploads",
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(900)
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Write);

        return blobClient.GenerateSasUri(sasBuilder).ToString();
    }
}