using EmailSender.Application.Common;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class UpdateTemplateCommand : IRequest<Result<NoContent>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Subject { get; set; }
        public Guid? AttributeId { get; set; }
    }
}
