using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMind.Data
{

    //Run Migrations from PMC
    //Add-Migration [migrationName] -Project MasterMind.Data -Context GameDataContext
    //Update-Database [migrationName] -Project MasterMind.Data -Context GameDataContext

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

            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.CodePeg1)
               .HasConversion(GamePattern.GetCodePegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.CodePeg2)
               .HasConversion(GamePattern.GetCodePegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.CodePeg3)
               .HasConversion(GamePattern.GetCodePegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.CodePeg4)
               .HasConversion(GamePattern.GetCodePegConverter());

            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.KeyPeg1)
               .HasConversion(GamePattern.GetKeyPegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.KeyPeg2)
               .HasConversion(GamePattern.GetKeyPegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.KeyPeg3)
               .HasConversion(GamePattern.GetKeyPegConverter());
            modelBuilder.Entity<GamePattern>()
               .Property(gp => gp.KeyPeg4)
               .HasConversion(GamePattern.GetKeyPegConverter());

            modelBuilder.Entity<GamePattern>()
                .HasOne(g => g.Game)
                .WithMany(p => p.Patterns)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
