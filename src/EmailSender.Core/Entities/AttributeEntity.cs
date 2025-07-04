using EmailSender.Core.Entities.Aggregates;
using EmailSender.Core.Entities.Common;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class AttributeEntity : EntityBase
    {
        public AttributeEntity(string name, IEnumerable<Field> fields)
        {
            Name = name;
            Fields = fields;
        }
        public string Name { get; set; }
        public IEnumerable<Field> Fields { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Template>()
                .Requires()
                    .IsNotNullOrEmpty(Name, nameof(Name), "Name is required.")
                    .IsNotEmpty(Fields, nameof(Fields), "Fields is required."));
        }
    }
}
