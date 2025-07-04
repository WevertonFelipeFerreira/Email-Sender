using EmailSender.Core.Entities.Common;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class Template : EntityBase
    {
        public Template(string name , string html, string subject)
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
            // TODO extract error messages to resource file
            AddNotifications(new Contract<Template>()
                .Requires()
                    .IsNotNullOrEmpty(Name, nameof(Name), "Name is required")
                    .IsNotNullOrEmpty(Html, nameof(Html), "Html is required")
                    .IsNotNullOrEmpty(Subject, nameof(Subject), "Subject is required"));
        }
    }
}