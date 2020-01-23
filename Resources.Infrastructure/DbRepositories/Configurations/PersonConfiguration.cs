using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resources.Domain.Models;

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
        }
    }
}
