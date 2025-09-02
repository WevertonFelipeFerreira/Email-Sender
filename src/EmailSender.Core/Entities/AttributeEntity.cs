using EmailSender.Core.Entities.Aggregates;
using EmailSender.Core.Entities.Common;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class AttributeEntity : EntityBase
    {
        public AttributeEntity(string name, IEnumerable<Field> fields, Guid userId)
        {
            Name = name;
            Fields = fields;
            UserId = userId;
        }

        public AttributeEntity(string name, IEnumerable<Field> fields, IEnumerable<Template> templates, Guid userId)
            : this(name, fields, userId)
        {
            Templates = templates;
        }

        public string Name { get; private set; }
        public IEnumerable<Field> Fields { get; private set; }
        public IEnumerable<Template> Templates { get; private set; } = new List<Template>();
        public Guid UserId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Template>()
                .Requires()
                    .IsNotNullOrEmpty(Name, nameof(Name), "Name is required.")
                    .IsNotEmpty(Fields, nameof(Fields), "Fields is required.")
                    .IsNotEmpty(UserId, "AuthorizedUser", "Invalid user."));
        }

        public void Update(string? name)
        {
            if (name != null)
                Name = name;

            SetModifiedDate();
            Validate();
        }
    }
}
