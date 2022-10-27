using BOX_Contatos.Models;
using Microsoft.EntityFrameworkCore;

namespace BOX_Contatos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> usuario { get; set; } = null!;
        public DbSet<Contatos> contatos { get; set; } = null!;
        public DbSet<Telefone> telefone { get; set; } = null!;
    }
}
