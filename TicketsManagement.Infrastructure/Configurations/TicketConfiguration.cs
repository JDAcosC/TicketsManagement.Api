using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketsManagement.Domain.Entities;
using TicketsManagement.Domain.Enums;

namespace TicketsManagement.Infrastructure.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {

            builder.ToTable("Tickets");


            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();


            builder.Property(t => t.User)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.UpdatedAt)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (TicketStatus)Enum.Parse(typeof(TicketStatus), v))
                .HasMaxLength(50);
        }
    }
}
