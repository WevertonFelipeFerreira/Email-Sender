using EmailSender.Api.ProblemDetail;
using EmailSender.Application.Commands;
using EmailSender.Application.Common;
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
        /// <returns>A body containing the created resource ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateAttributeCommand command)
        {
            var result = await mediator.Send(command);

            return result.ErrorType switch
            {
                EErrorType.NOTIFICATION_ERROR => BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications)),
                _ => Created(nameof(GetById), result.Value)
            };
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

            return result.ErrorType switch
            {
                EErrorType.NOT_FOUND => NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not Found", "Could not found attribute with the given id.")),
                _ => Ok(result.Value)
            };
        }

        /// <summary>
        /// Update a attribute
        /// </summary>
        /// <param name="id">Attribute identifier</param>
        /// <param name="request">Request body to update</param>
        /// <returns>No content result</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAttributeCommand request)
        {
            request.Id = id;
            var result = await mediator.Send(request);

            return result.ErrorType switch
            {
                EErrorType.NOTIFICATION_ERROR => BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications)),
                EErrorType.NOT_FOUND => NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not found", "Could not found attribute with the given id.")),
                EErrorType.CONFLICT => Conflict(ApiError.CreateProblem(HttpContext, HttpStatusCode.Conflict, "Conflict", "This attribute set is already associated with a template and cannot be modified.")),
                _ => NoContent()
            };
        }

        /// <summary>
        /// Delete a attribute
        /// </summary>
        /// <param name="id">Attribute identifier</param>
        /// <param name="request">Request body to update</param>
        /// <returns>No content result</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteAttributeCommand request = new() { Id = id };
            var result = await mediator.Send(request);

            return result.ErrorType switch
            {
                EErrorType.NOT_FOUND => NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not found", "Could not found attribute with the given id.")),
                _ => NoContent()
            };
        }

        /// <summary>
        /// Retrieves a paginated list of attributes with optional filters.
        /// </summary>
        /// <param name="page">The page number to retrieve (starting from 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="name">Optional filter to search attributes by name (partial match).</param>
        /// <param name="fieldNames">
        /// Optional filter to match attributes associated with specific field names. 
        /// Provide multiple names separated by commas (e.g., "Email,FirstName").
        /// </param>
        /// <returns>
        /// Returns a <see cref="Paged{AttributeViewModel}"/> object containing the filtered and paginated attributes.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(Paged<AttributeViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null, [FromQuery] string? fieldNames = null)
        {
            GetAttributeQuery request = new(page, pageSize, name);
            var result = await mediator.Send(request);

            return Ok(result.Value);
        }
    }
}