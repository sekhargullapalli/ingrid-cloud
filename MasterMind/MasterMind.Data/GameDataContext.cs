using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMind.Data
{
    public class GameDataContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePattern> GamePatterns { get; set; }
        public GameDataContext(DbContextOptions<GameDataContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

        }
    }
}
