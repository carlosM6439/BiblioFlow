using Microsoft.EntityFrameworkCore;
using BiblioFlow.API.Models;

namespace BiblioFlow.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
    }
}