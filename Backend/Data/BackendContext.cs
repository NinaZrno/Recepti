

using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> opcije) : base(opcije)
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
            modelBuilder.Entity<Sastav>().HasOne(g => g.Recept);
            modelBuilder.Entity<Sastav>().HasOne(g => g.Sastojak);



        }


    }
}
    

