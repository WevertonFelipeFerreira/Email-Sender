using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class CreateTemplateCommand : IRequest<IdModel>
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string[]? SupportedAttributes { get; set; }
    }
}