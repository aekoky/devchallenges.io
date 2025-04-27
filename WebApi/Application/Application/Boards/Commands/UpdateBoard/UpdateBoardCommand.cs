using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Domain.Entities;
using MyTaskBoard.Domain.Enums;
using MediatR;

namespace MyTaskBoard.Application.Application.Boards.Commands.UpdateBoard;

public record UpdateBoardCommand : IRequest
{
    public int Id { get; init; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}

public class UpdateBoardCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateBoardCommand>
{
    public async Task Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var BoardAction = await dbContext.Boards
            .SingleOrDefaultAsync(board => board.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Board), request.Id);

        BoardAction.Name = request.Name ?? BoardAction.Name;
        BoardAction.Description = request.Description;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
