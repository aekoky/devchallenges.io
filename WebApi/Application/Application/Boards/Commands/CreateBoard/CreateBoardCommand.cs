using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;
using MediatR;

namespace MyTaskBoard.Application.Application.Boards.Commands.CreateBoard;

public record CreateBoardCommand : IRequest<int>
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}

public class CreateBoardCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateBoardCommand, int>
{
    public async Task<int> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = new Board
        {
            Name = request.Name,
            Description = request.Description
        };

        dbContext.Boards.Add(board);

        await dbContext.SaveChangesAsync(cancellationToken);

        return board.Id;
    }
}
