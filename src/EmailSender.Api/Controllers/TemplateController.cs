using EmailSender.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.Controllers
{
    [ApiController]
    [Route("template")]
    public class TemplateController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTemplateCommand command)
        {
            var response = await mediator.Send(command);
            return Created("", response);
        }
    }
}
