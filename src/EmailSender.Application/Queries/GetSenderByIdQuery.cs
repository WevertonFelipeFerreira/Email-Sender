using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Queries
{
    public class GetSenderByIdQuery : IRequest<Result<SenderViewModel>>
    {
        public Guid Id { get; set; }
    }
}
