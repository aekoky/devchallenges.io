using MyTaskBoard.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTaskBoard.Domain.Entities;
using MyTaskBoard.Domain.Enums;

namespace MyTaskBoard.Persistence.Initialization;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                var migrations = await _context.Database.GetPendingMigrationsAsync();
                if (migrations.Any())
                    await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.Execute(async () =>
                {
                    var transaction = await _context.Database.BeginTransactionAsync();
                    await TrySeedAsync();
                    await transaction.CommitAsync();
                });
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var board = await _context.Boards
            .Include(board => board.Tasks)
            .AsNoTracking()
            .OrderBy(board => board.Id)
            .FirstOrDefaultAsync();

        if (board is null)
        {
            // Default roles
            board = new Board()
            {
                Name = "My Task Board",
                Description = "Tasks to keep organised"
            };

            var boardEntityEntry = _context.Boards.Add(board);
            if (await _context.SaveChangesAsync() > 0)
            {
                board.Tasks.AddRange([
                    new BoardTask{
                        Name="Task in Progress",
                        Status=BoardTaskStatus.InProgress,
                        Icon=BoardTaskIcon.Icon1,
                        BoardId=boardEntityEntry.Entity.Id
                    },
                    new BoardTask{
                        Name="Task Completed",
                        Status=BoardTaskStatus.Done,
                        Icon=BoardTaskIcon.Icon2,
                        BoardId=boardEntityEntry.Entity.Id
                    },
                    new BoardTask{
                        Name="Task Won’t Do",
                        Status=BoardTaskStatus.Refused,
                        Icon=BoardTaskIcon.Icon3,
                        BoardId=boardEntityEntry.Entity.Id
                    },
                    new BoardTask{
                        Name="Task To Do",
                        Status=BoardTaskStatus.Pending,
                        Icon=BoardTaskIcon.Icon4,
                        Description="",
                        BoardId=boardEntityEntry.Entity.Id
                    },

                ]);
                await _context.SaveChangesAsync();
            }
        }
    }
}
