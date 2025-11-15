using MediatR;
using Upload.Application.DTOs;
using Upload.Application.Interfaces;
using Upload.Domain.Entities;
using Upload.Domain.Repositories;

namespace Upload.Application.Commands.InitUpload;

public class InitUploadHandler : IRequestHandler<InitUploadCommand, InitUploadResponse>
{
    private readonly IContentRepository _contentRepository;
    private readonly IDuplicateCheckService _duplicateCheckService;
    private readonly IBlobStorageService _blobStorageService;

    public InitUploadHandler(
        IContentRepository contentRepository,
        IDuplicateCheckService duplicateCheckService,
        IBlobStorageService blobStorageService)
    {
        _contentRepository = contentRepository;
        _duplicateCheckService = duplicateCheckService;
        _blobStorageService = blobStorageService;
    }

    public async Task<InitUploadResponse> Handle(InitUploadCommand request, CancellationToken cancellationToken)
    {
        if (request.Type != "text" && request.Type != "image")
            throw new ArgumentException("Type must be 'text' or 'image'");

        var isDuplicate = await _duplicateCheckService.CheckDuplicateAsync(request.ContentHash);
        
        if (isDuplicate)
        {
            return new InitUploadResponse
            {
                ContentId = string.Empty,
                UploadUrl = string.Empty,
                ExpiresIn = 900,
                IsDuplicate = true
            };
        }

        var contentId = Guid.NewGuid().ToString();
        
        var content = new Content
        {
            id = contentId,
            contentId = contentId,
            ContentId = contentId,
            Type = request.Type,
            FileName = request.FileName,
            ContentHash = request.ContentHash,
            Status = "Uploading",
            CreatedAt = DateTime.UtcNow
        };

        await _contentRepository.CreateAsync(content);
        
        var uploadUrl = await _blobStorageService.GenerateUploadUrlAsync(request.FileName, contentId);
        
        await _duplicateCheckService.SetDuplicateAsync(request.ContentHash);

        return new InitUploadResponse
        {
            ContentId = contentId,
            UploadUrl = uploadUrl,
            ExpiresIn = 900,
            IsDuplicate = false
        };
    }
}