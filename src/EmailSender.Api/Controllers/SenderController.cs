using EmailSender.Api.ProblemDetail;
using EmailSender.Application.Commands;
using EmailSender.Application.Dtos.ViewModels;
using EmailSender.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
