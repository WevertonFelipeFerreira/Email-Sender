using EmailSender.Application.Common;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class DeleteTemplateCommand : IRequest<Result<NoContent>>
    {
        public Guid Id { get; set; }
    }
}
