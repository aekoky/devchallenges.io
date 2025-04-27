using MyTaskBoard.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Domain.Entities;
using System.Reflection;

namespace MyTaskBoard.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Board> Boards => Set<Board>();

    public DbSet<BoardTask> BoardTasks => Set<BoardTask>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await Database.BeginTransactionAsync(cancellationToken);
    }
}

//add-migration Initial -outputdir Migrations/Application -context ApplicationDbContext
//add-migration version-1 -outputdir Migrations/Application -context ApplicationDbContext
//add-migration Initial -outputdir Migrations/MultiTenant -context MultiTenantStoreDbContext
//script-migration -context ApplicationDbContext
//remove-migration -context ApplicationDbContext
//update-database -context ApplicationDbContext