using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Queries
{
    public class GetAttributeByIdQuery : IRequest<Result<AttributeViewModel>>
    {
        public Guid Id { get; set; }
    }
}
