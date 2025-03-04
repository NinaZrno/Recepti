

using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Data
{
    public class EdunovaContext : DbContext
    {
        public EdunovaContext(DbContextOptions<EdunovaContext> opcije) : base(opcije)
        {

        }


        public DbSet<Recept> Recepti
        { get; set; }

        public DbSet<Sastav> Sastavi
        { get; set; }

        public DbSet<Sastojak> Sastojci
        { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // implementacija veze 1:n
            modelBuilder.Entity<Sastojak>().HasOne(g => g.Recept);

            // implementacija veze n:n
            modelBuilder.Entity<Sastojak>()
                .HasMany(g => g.Sastav)
                .WithMany(p => p.Sastojak)
                .UsingEntity<Dictionary<string, object>>("sastavi",
                c => c.HasOne<Polaznik>().WithMany().HasForeignKey("sastav"),
                c => c.HasOne<Grupa>().WithMany().HasForeignKey("sastojak"),
                c => c.ToTable("sastavi")
                );

        }


    }
}
    

