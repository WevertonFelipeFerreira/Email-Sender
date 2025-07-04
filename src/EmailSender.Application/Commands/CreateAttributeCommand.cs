using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;

namespace EmailSender.Application.Commands
{
    public class CreateAttributeCommand : IRequest<Result<IdResponseModel>>
    {
        public string? Name { get; set; } = null;
        public IEnumerable<FieldModel> Fields { get; set; } = new List<FieldModel>();
    }
    public record FieldModel(string name, bool required);
}
