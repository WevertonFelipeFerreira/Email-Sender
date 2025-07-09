using EmailSender.Application.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace EmailSender.Application.Commands
{
    public class UpdateAttributeCommand : IRequest<Result<NoContent>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; } = null;
        public IEnumerable<FieldModel> Fields { get; set; } = new List<FieldModel>();
    }
}