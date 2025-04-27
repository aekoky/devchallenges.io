using MediatR;
using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyTaskBoard.Application.Application.BoardTasks.Commands.DeleteBoardTask;

public record DeleteBoardTaskCommand(int Id) : IRequest;

public class DeleteBoardTaskCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteBoardTaskCommand>
{
    public async Task Handle(DeleteBoardTaskCommand request, CancellationToken cancellationToken)
    {
        var boardTask = await dbContext.BoardTasks
            .SingleOrDefaultAsync(boardTask => boardTask.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(BoardTask), request.Id);
        var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.BoardTasks.Remove(boardTask);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            throw;
        }
    }

}
