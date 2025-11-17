using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketsManagement.Infrastructure.Persistence;

#nullable disable

namespace TicketsManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("ProductVersion", "8.0.22");

            modelBuilder.Entity("TicketsManagement.Domain.Entities.Ticket", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("User")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.ToTable("Tickets", (string)null);
            });

        }
    }
}
