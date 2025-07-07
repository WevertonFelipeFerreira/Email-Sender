using EmailSender.Api.ProblemDetail;
using EmailSender.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.Controllers
{
    [ApiController]
    [Route("attributes")]
    public class AttributeController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a attribute
        /// </summary>
        /// <param name="command">Body of the request</param>
        /// <returns>A boddy containing the created resource id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateAttributeCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateAttributeCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.IsValid && result.Notifications.Any())
                return BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications));

            return Created("", result.Value); // TODO update empty string to the get by id route when added.
        }
    }
}
