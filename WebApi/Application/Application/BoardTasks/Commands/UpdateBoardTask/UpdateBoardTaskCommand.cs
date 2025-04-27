using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Domain.Entities;
using MediatR;
using MyTaskBoard.Domain.Enums;

namespace MyTaskBoard.Application.Application.BoardTasks.Commands.UpdateBoardTask;

public record UpdateBoardTaskCommand : IRequest
{
    public int Id { get; init; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public BoardTaskIcon? Icon { get; set; }

    public BoardTaskStatus? Status { get; set; }
}

public class UpdateBoardTaskCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateBoardTaskCommand>
{
    public async Task Handle(UpdateBoardTaskCommand request, CancellationToken cancellationToken)
    {
        var boardTaskAction = await dbContext.BoardTasks
            .SingleOrDefaultAsync(boardTask => boardTask.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(BoardTask), request.Id);

        boardTaskAction.Name = request.Name ?? boardTaskAction.Name;
        boardTaskAction.Description = request.Description;
        boardTaskAction.Icon = request.Icon ?? boardTaskAction.Icon;
        boardTaskAction.Status = request.Status ?? boardTaskAction.Status;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
