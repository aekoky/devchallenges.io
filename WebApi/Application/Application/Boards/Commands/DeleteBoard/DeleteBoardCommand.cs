using MediatR;
using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyTaskBoard.Application.Application.Boards.Commands.DeleteBoard;

public record DeleteBoardCommand(int Id) : IRequest;

public class DeleteBoardCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteBoardCommand>
{
    public async Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await dbContext.Boards
            .Include(board => board.Tasks)
            .SingleOrDefaultAsync(board => board.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Board), request.Id);
        var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.BoardTasks.RemoveRange(board.Tasks);
            await dbContext.SaveChangesAsync(cancellationToken);

            dbContext.Boards.Remove(board);
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
