using EmailSender.Core.Entities.Common;
using Flunt.Validations;

namespace EmailSender.Core.Entities
{
    public class Template : EntityBase
    {
        public Template(string name, string html)
        {
            Name = name;
            Html = html;
        }

        public Template(string name, string content, string[]? supportedAttributes) : this(name, content)
        {
            SupportedAttributes = supportedAttributes;
        }

        public string Name { get; private set; }
        public string Html { get; private set; }
        public string[]? SupportedAttributes { get; private set; }

        public override void Validate()
        {
            // TODO extract error messages to resource file
            new Contract<Template>()
                .Requires()
                .IsNullOrEmpty(Name, nameof(Name), "Name is required")
                .IsNullOrEmpty(Html, nameof(Html), "Html is required");
        }
    }
}