using EmailSender.Core.Entities.Aggregates;
using EmailSender.Core.Entities.Common;
using Flunt.Notifications;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class Template : EntityBase
    {
        public Template(string name, string html, string subject)
        {
            Name = name;
            Html = html;
            Subject = subject;
        }

        public Template(string name, string html, string subject, Guid attributeId)
            : this(name, html, subject)
        {
            AttributeId = attributeId;
        }

        public Template()
        {

        }

        public string Name { get; private set; }
        public string Html { get; private set; }
        public string Subject { get; private set; }
        public AttributeEntity? Attribute { get; private set; }
        public Guid? AttributeId { get; private set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Template>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsNotNullOrWhiteSpace(Html, nameof(Html), "Html is required")
                .IsNotNullOrWhiteSpace(Subject, nameof(Subject), "Subject is required"));
        }

        public void ValidateAttributes(IEnumerable<Field> fields)
        {
            var missingFields = fields
                .Where(x => x.Required && !Html.Contains($"{{{{{x.Name}}}}}"))
                .Select(field => new Notification("Attributes", $"The attribute {{{{{field.Name}}}}} is required."));

            if (missingFields.Any())
                missingFields.ToList().ForEach(AddNotification);
        }

        public void SanitizeHtml(Func<string, string> sanitizer)
        {
            Html = sanitizer(Html);
        }
    }
}