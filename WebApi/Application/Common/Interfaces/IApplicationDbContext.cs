using MyTaskBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyTaskBoard.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Board> Boards { get; }

    DbSet<BoardTask> BoardTasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
