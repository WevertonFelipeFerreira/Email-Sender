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
    [Route("attributes")]
    public class AttributeController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a attribute
        /// </summary>
        /// <param name="command">Body of the request</param>
        /// <returns>A boddy containing the created resource id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateAttributeCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.IsValid && result.Notifications.Any())
                return BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications));

            return Created(nameof(GetById), result.Value);
        }

        /// <summary>
        /// Get a attribute by your identifier
        /// </summary>
        /// <param name="id">Attribute identifier</param>
        /// <returns>The requested Attribute</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttributeViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetAttributeByIdQuery { Id = id };
            var result = await mediator.Send(request);

            if (result.ErrorType == EErrorType.NOT_FOUND)
                return NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not Found", "Could not found attribute with the given id."));

            return Ok(result.Value);
        }
    }
}
