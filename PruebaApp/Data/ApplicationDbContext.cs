using PruebaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PruebaApp.Data

{
    public class ApplicationDbContext : DbContext
    {
        //Dbcontext se encarga de usar EF Core para usar las tablas de la base de datos como objetos y a partir del objeto ApplicationDbContext se puede acceder a estos objetos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Productos> productos { get; set; }
        public DbSet<Clientes> clientes { get; set; }

        public DbSet<PedidoProductos> pedidoProductos { get; set; }

        public DbSet<Pedidos> pedidos { get; set; }
    }
}
