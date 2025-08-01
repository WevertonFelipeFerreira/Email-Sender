﻿using EmailSender.Api.ProblemDetail;
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
    [Route("templates")]
    public class TemplateController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create a email template
        /// </summary>
        /// <param name="command">Body of the request</param>
        /// <returns>A body containing the created resource ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IdResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateTemplateCommand command)
        {
            var result = await mediator.Send(command);

            return result.ErrorType switch
            {
                EErrorType.NOTIFICATION_ERROR => BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications)),
                _ => CreatedAtAction(nameof(GetById), new { Id = result.Value!.id }, result.Value)
            };
        }

        /// <summary>
        /// Get a template by your identifier
        /// </summary>
        /// <param name="id">Template identifier</param>
        /// <returns>The requested temmplate</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TemplateViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetTemplateByIdQuery { Id = id };
            var result = await mediator.Send(request);

            return result.ErrorType switch
            {
                EErrorType.NOT_FOUND => NotFound(ApiError.CreateProblem(HttpContext, HttpStatusCode.NotFound, "Not Found", "Could not found template with the given id.")),
                _ => Ok(result.Value)
            };
        }
    }
}