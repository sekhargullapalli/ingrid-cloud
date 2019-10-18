using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MasterMind.Data
{   
    public class GameDataContextFactory : IDesignTimeDbContextFactory<GameDataContext>
    {
        public GameDataContextFactory() { }

        //For testing, using sqlite
        public GameDataContext CreateDbContext(string[] args)
        {
            string connectionString = @"Data Source=gamedb.db";
            var optionsBuilder = new DbContextOptionsBuilder<GameDataContext>();
            optionsBuilder.UseSqlite(connectionString);
            return new GameDataContext(optionsBuilder.Options);
        }
    }
}
