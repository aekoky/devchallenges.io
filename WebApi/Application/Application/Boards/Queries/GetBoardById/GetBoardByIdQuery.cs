using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Application.Common.Exceptions;
using MyTaskBoard.Application.Common.DTOs;
using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Domain.Entities;

namespace MyTaskBoard.Application.Application.Boards.Queries.GetBoardById;

public record GetBoardByIdQuery(int Id) : IRequest<BoardDto>;

public class GetBoardByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetBoardByIdQuery, BoardDto>
{
    public async Task<BoardDto> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
    {
        var board = await dbContext.Boards
            .Include(board => board.Tasks)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Board), request.Id);

        return mapper.Map<BoardDto>(board);
    }
}
