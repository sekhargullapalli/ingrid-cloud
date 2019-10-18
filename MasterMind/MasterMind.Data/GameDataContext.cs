using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMind.Data
{
    class GameDataContext: DbContext
    {
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
