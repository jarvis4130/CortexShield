using MediatR;
using Content.Application.DTOs;
using Content.Domain.Repositories;

namespace Content.Application.Commands.DeleteContent;

public class DeleteContentHandler : IRequestHandler<DeleteContentCommand, SuccessResponse>
{
    private readonly IContentRepository _contentRepository;

    public DeleteContentHandler(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<SuccessResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
    {
        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        
        if (content == null)
        {
            throw new KeyNotFoundException($"Content with ID {request.ContentId} not found");
        }

        content.Status = "Deleted";
        content.DeletedAt = DateTime.UtcNow;

        await _contentRepository.UpdateAsync(content);

        return new SuccessResponse(true);
    }
}