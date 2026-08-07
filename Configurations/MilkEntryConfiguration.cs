using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Configurations;

public class MilkEntryConfiguration : IEntityTypeConfiguration<MilkEntry>
{
    public void Configure(EntityTypeBuilder<MilkEntry> builder)
    {
        builder.ToTable("MilkEntries");

        builder.HasKey(m => m.MilkEntryId);

        builder.Property(m => m.MilkEntryId)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.MilkQuantity)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(m => m.ShiftType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(m => m.Date)
            .IsRequired();

        builder.HasOne(m => m.Cow)
            .WithMany()
            .HasForeignKey(m => m.FId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
