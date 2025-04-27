using MyTaskBoard.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyTaskBoard.Persistence.Common;

public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=MyTaskBoardDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}