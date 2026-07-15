using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Pessoa> Pessoas => Set<Pessoa>();
        public DbSet<Transacao> Transacoes => Set<Transacao>();
    }
}
