using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Entity;

namespace MoviesWebApp.Data
{
    public class MovieContext : DbContext
    {

        public MovieContext(DbContextOptions<MovieContext> options): base(options)//2. yöntem sql bağlantıyı dışardan vermek istiyorsak bu söz dizimi gereklidir
        {
                
        }
        public DbSet<Movie> MovieSet { get; set; } // burada olmayan veri tabanı var ise otomatik olarak oluşturulmuş oluyor.
        //bunuda Entityframework core aracılığı ile yapıyoruz.
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//1. yöntem sql bağlantıyı içerden vermek istiyorsak bu söz dizimi yeterlidir
        //{
        //    optionsBuilder.UseSqlite("Data Source = movie.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(b => b.Title).HasMaxLength(500);

            modelBuilder.Entity<Genre>().Property(b => b.name).IsRequired();
            modelBuilder.Entity<Genre>().Property(b => b.name).HasMaxLength(50);
        }


    }
}
