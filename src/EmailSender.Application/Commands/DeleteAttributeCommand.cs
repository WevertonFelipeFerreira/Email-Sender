using EmailSender.Application.Common;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class DeleteAttributeCommand : IRequest<Result<NoContent>>
    {
        public Guid Id { get; set; }
    }
}