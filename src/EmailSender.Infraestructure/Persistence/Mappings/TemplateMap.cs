using EmailSender.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace EmailSender.Infraestructure.Persistence.Mappings
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Template");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SupportedAttributes)
                .HasConversion(
                    supportedAttributesArray =>
                        supportedAttributesArray != null && supportedAttributesArray.Length > 0
                            ? JsonConvert.SerializeObject(supportedAttributesArray)
                            : null,

                    attributesJson =>
                        string.IsNullOrEmpty(attributesJson)
                            ? Array.Empty<string>()
                            : JsonConvert.DeserializeObject<string[]>(attributesJson)
            );

            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
