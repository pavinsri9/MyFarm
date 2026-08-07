using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Configurations;

public class CowConfiguration : IEntityTypeConfiguration<Cow>
{
    public void Configure(EntityTypeBuilder<Cow> builder)
    {
        builder.ToTable("Cows");

        builder.HasKey(c => c.PId);

        builder.Property(c => c.PId)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.CowName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
