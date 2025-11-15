using MediatR;
using Microsoft.AspNetCore.Mvc;
using Upload.Application.Commands.InitUpload;
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
}