using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;
using MediatR;
using MyTaskBoard.Domain.Enums;

namespace MyTaskBoard.Application.Application.BoardTasks.Commands.CreateBoardTask;

public record CreateBoardTaskCommand : IRequest<int>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public BoardTaskIcon Icon { get; set; }

    public BoardTaskStatus Status { get; set; }

    public int BoardId { get; set; }
}

public class CreateBoardTaskCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateBoardTaskCommand, int>
{
    public async Task<int> Handle(CreateBoardTaskCommand request, CancellationToken cancellationToken)
    {
        var boardTask = new BoardTask
        {
            Name = request.Name,
            Description = request.Description,
            Icon = request.Icon,
            Status = request.Status,
            BoardId = request.BoardId
        };

        dbContext.BoardTasks.Add(boardTask);

        await dbContext.SaveChangesAsync(cancellationToken);

        return boardTask.Id;
    }
}
