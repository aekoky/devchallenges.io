namespace MyTaskBoard.Domain.Entities;

public class Board : BaseAuditableEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public virtual List<BoardTask> Tasks { get; set; } = [];
}
