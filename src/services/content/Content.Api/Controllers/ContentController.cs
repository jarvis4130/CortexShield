using MediatR;
using Microsoft.AspNetCore.Mvc;
using Content.Application.DTOs;
using Content.Application.Queries.GetContent;
using Content.Application.Commands.DeleteContent;
using Content.Application.Commands.UpdateContentStatus;

namespace Content.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{contentId}")]
    public async Task<ActionResult<ContentResponse>> GetContent(string contentId)
    {
        try
        {
            var query = new GetContentQuery(contentId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{contentId}")]
    public async Task<ActionResult<SuccessResponse>> DeleteContent(string contentId)
    {
        try
        {
            var command = new DeleteContentCommand(contentId);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{contentId}/status")]
    public async Task<ActionResult<UpdatedResponse>> UpdateContentStatus(string contentId, [FromBody] UpdateStatusRequest request)
    {
        try
        {
            var command = new UpdateContentStatusCommand(
                contentId,
                request.Status,
                request.CombinedScore,
                request.ConfidenceScore,
                request.Priority,
                request.ReviewQueueId
            );
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}