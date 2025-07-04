using EmailSender.Core.Entities;
using EmailSender.Core.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace EmailSender.Infraestructure.Persistence.Mappings
{
    public class AttributeMap : IEntityTypeConfiguration<AttributeEntity>
    {
        public void Configure(EntityTypeBuilder<AttributeEntity> builder)
        {
            builder.ToTable("Attribute");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fields)
                .HasConversion(
                    supportedAttributesArray =>
                        supportedAttributesArray != null && supportedAttributesArray.Any()
                            ? JsonConvert.SerializeObject(supportedAttributesArray)
                            : null,

                    attributesJson =>
                        string.IsNullOrEmpty(attributesJson)
                            ? new List<Field>()
                            : JsonConvert.DeserializeObject<IEnumerable<Field>>(attributesJson)!
            );

            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
