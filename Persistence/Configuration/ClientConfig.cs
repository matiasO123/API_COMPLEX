using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class ClientConfig :IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.id);

            builder.Property(x => x.name).
                HasMaxLength(80)
                .IsRequired();

            builder.Property(x => x.birthDate).IsRequired();

            builder.Property(x=> x.phone_number).HasMaxLength(45).IsRequired();

            builder.Property(x => x.email).IsRequired().HasMaxLength(100);

            builder.Property(x => x.adress).IsRequired().HasMaxLength(120);
            builder.Property(x => x.age);

            builder.Property(x => x.CreatedBy).HasMaxLength(30);

            builder.Property(x => x.LastModifiedBy).HasMaxLength(30);
        }
    }
}
