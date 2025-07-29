using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Queries
{
    public class GetTemplateByIdQuery : IRequest<Result<TemplateViewModel>>
    {
        public Guid Id { get; set; }
    }
}
