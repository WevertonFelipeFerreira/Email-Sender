using EmailSender.Api.ProblemDetail;
using EmailSender.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.Controllers
{
    [ApiController]
    [Route("templates")]
    public class TemplateController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTemplateCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.IsValid && result.Notifications.Any())
                return BadRequest(ApiError.CreateValidationProblem(HttpContext, result.Notifications));

            return Created("", result.Value); // TODO update empty string to the get by id route when added.
        } 
    }
}
