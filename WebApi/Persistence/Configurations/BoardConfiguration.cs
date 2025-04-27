using MyTaskBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyTaskBoard.Persistence.Configurations;
public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Board");

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Description).HasMaxLength(1500);

        builder.HasMany(e => e.Tasks)
            .WithOne(e => e.Board)
            .HasForeignKey(e => e.BoardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
