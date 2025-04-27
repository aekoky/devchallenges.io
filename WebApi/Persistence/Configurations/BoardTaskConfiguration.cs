using MyTaskBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyTaskBoard.Persistence.Configurations;
public class BoardTaskConfiguration : IEntityTypeConfiguration<BoardTask>
{
    public void Configure(EntityTypeBuilder<BoardTask> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("BoardTask");

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Description).HasMaxLength(1500);

        builder.HasOne(e => e.Board)
            .WithMany(e => e.Tasks)
            .HasForeignKey(e => e.BoardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
