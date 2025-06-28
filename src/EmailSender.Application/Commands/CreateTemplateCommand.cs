using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class CreateTemplateCommand : IRequest<Result<IdModel>>
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Subject { get; set; }
        public string[]? SupportedAttributes { get; set; }
    }
}