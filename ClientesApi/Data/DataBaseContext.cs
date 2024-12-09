using ClientesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<Cupon_ClienteModel> Cupones_Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientesModel>()
                .HasKey(c => c.CodCliente);

            modelBuilder.Entity<Cupon_ClienteModel>()
            .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
