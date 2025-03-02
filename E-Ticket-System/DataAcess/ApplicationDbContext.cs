using E_Ticket_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace E_Ticket_System.DataAcess
{
    public class ApplicationDbContext:DbContext

    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<ActorMovies> ActorMovies { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=E-Tickets;Integrated Security=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");  
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Cinema>().ToTable("Cinema");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<ActorMovies>().ToTable("ActorMovies");
            modelBuilder.Entity<ActorMovies>()
            .HasKey(e => new { e.ActorId, e.MovieId });
            modelBuilder.Entity<ActorMovies>()
    .HasOne(am => am.actor)
    .WithMany(a => a.ActorMovies)
    .HasForeignKey(am => am.ActorId); 

            modelBuilder.Entity<ActorMovies>()
                .HasOne(am => am.movie)
                .WithMany(m => m.ActorMovies)
                .HasForeignKey(am => am.MovieId);
        }
    }
}
