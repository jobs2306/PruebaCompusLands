namespace PruebaApp.Models
{
    public class Pedidos
    {
        //Id (PK, int), ClienteId (FK), FechaPedido (DateTime),
        //Total(decimal calculado).

        public int Id { get; set; }
        public int ClientesId { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }

        // Relación con Clientes
        public Clientes? Cliente { get; set; }

        // Relación con PedidoProductos
        public ICollection<PedidoProductos>? PedidoProductos { get; set; }
    }
}
