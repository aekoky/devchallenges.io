namespace MyTaskBoard.Domain.Entities;

public class BoardTask : BaseAuditableEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public BoardTaskIcon Icon { get; set; }

    public BoardTaskStatus Status { get; set; }

    public int BoardId { get; set; }

    public virtual Board? Board { get; set; }
}
