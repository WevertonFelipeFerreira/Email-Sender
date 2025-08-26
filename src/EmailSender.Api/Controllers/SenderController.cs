using EmailSender.Api.ProblemDetail;
using EmailSender.Application.Commands;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Application.Queries;
using EmailSender.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmailSender.Api.Controllers
{
    [ApiController]
    [Route("senders")]
    public class SenderController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a sender
        /// </summary>
        /// <param name="command">Body of the request</param>
        /// <returns>A body containing the created resource ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateSenderCommand command)
        {
            var result = await mediator.Send(command);

            return result.ErrorType switch
            {
                EErrorType.NOTIFICATION_ERROR => BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications)),
                _ => CreatedAtAction(nameof(GetById), new { Id = result.Value!.id }, result.Value)
            };
        }

        /// <summary>
        /// Get a sender by your identifier
        /// </summary>
        /// <param name="id">Sender identifier</param>
        /// <returns>The requested sender</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SenderViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetSenderByIdQuery { Id = id };
            var result = await mediator.Send(request);

            return result.ErrorType switch
            {
                EErrorType.NOT_FOUND => NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not Found", "Could not found sender with the given id.")),
                _ => Ok(result.Value)
            };
        }
    }
}
