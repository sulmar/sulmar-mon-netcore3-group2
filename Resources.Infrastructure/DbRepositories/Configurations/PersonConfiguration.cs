using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Resources.Domain.Models;
using System;

namespace Resources.Infrastructure.DbRepositories.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
              .Property(p => p.FirstName)
              .HasMaxLength(40);

            builder
                .Property(p => p.LastName)
                .HasMaxLength(40)
                .IsRequired();

            //builder
            //    .HasData()


            // konwerter za użyciem wyrażeń lambda
            builder
                .Property(p => p.Gender)
                .HasConversion
                (
                    v => v.ToString(),
                    v => (Gender) Enum.Parse(typeof(Gender), v)
                ) ;

            // konwerter wbudowany
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.valueconversion?view=efcore-3.1
            builder
                .Property(p => p.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());


            builder.Property(p => p.WorkAddress)
                .HasConversion(
                 v => JsonConvert.SerializeObject(v),
                 v => JsonConvert.DeserializeObject<Address>(v)
                );

        }
    }
}
