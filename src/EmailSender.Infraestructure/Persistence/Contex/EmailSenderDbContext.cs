using EmailSender.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmailSender.Infraestructure.Persistence.Contex
{
    public class EmailSenderDbContext : DbContext
    {
        public EmailSenderDbContext(DbContextOptions<EmailSenderDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Template> Templates { get; set; } = null!;
        public DbSet<AttributeEntity> Attributes { get; set; } = null!;
    }
}
