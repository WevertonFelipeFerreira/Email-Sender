using EmailSender.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailSender.Infraestructure.Persistence.Mappings
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Template");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Attribute)
                .WithMany(x => x.Templates)
                .HasForeignKey(x => x.AttributeId);

            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
