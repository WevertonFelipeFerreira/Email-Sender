using EmailSender.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailSender.Infraestructure.Persistence.Mappings
{
    public class SenderMap : IEntityTypeConfiguration<Sender>
    {
        public void Configure(EntityTypeBuilder<Sender> builder)
        {
            builder.ToTable("Sender");
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
