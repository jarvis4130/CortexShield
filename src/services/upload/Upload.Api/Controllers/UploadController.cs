using MediatR;
using Microsoft.AspNetCore.Mvc;
using Upload.Application.Commands.InitUpload;
using Upload.Application.Commands.CompleteUpload;
using Upload.Application.Queries.GetContent;
using Upload.Application.DTOs;

namespace Upload.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("init")]
    public async Task<ActionResult<InitUploadResponse>> InitUpload([FromBody] InitUploadRequest request)
    {
        var command = new InitUploadCommand
        {
            Type = request.Type,
            FileName = request.FileName,
            ContentLength = request.ContentLength,
            ContentHash = request.ContentHash
        };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("complete")]
    public async Task<ActionResult<CompleteUploadResponse>> CompleteUpload([FromBody] CompleteUploadRequest request)
    {
        try
        {
            var command = new CompleteUploadCommand
            {
                ContentId = request.ContentId,
                BlobUrl = request.BlobUrl
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{contentId}")]
    public async Task<ActionResult<GetContentResponse>> GetContent(string contentId)
    {
        try
        {
            var query = new GetContentQuery
            {
                ContentId = contentId
            };

            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}