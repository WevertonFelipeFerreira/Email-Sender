using EmailSender.Application.Common;
using EmailSender.Application.Dtos.ViewModels;
using MediatR;
using System.Text.Json.Serialization;

namespace EmailSender.Application.Commands
{
    public class CreateAttributeCommand : IRequest<Result<IdResponseModel>>
    {
        public string? Name { get; set; } = null;
        public IEnumerable<FieldModel> Fields { get; set; } = new List<FieldModel>();

        [JsonIgnore]
        public Guid? UserId { get; set; }
    }
    public record FieldModel(string name, bool required);
}
