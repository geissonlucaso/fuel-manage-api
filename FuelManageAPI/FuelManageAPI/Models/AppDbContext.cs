using Microsoft.EntityFrameworkCore;

namespace FuelManageAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
    }
}
