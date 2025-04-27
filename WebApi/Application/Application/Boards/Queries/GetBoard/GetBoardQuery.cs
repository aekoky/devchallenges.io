using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.DTOs;
using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;

namespace MyTaskBoard.Application.Application.Boards.Queries.GetBoard;

public record GetBoardQuery : IRequest<BoardDto>;

public class GetBoardQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetBoardQuery, BoardDto>
{
    public async Task<BoardDto> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        var board = await dbContext.Boards
            .Include(board => board.Tasks)
            .AsNoTracking()
            .OrderBy(board => board.Id)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(Board));

        return mapper.Map<BoardDto>(board);
    }
}
