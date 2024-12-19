namespace PruebaApp.Models
{
    public class PedidoProductos
    {
        //PedidoId (FK), ProductoId (FK), Cantidad (int).
        public int id { get; set; }
        public int PedidosId { get; set; }
        public int ProductosId { get; set; }
        public int Cantidad { get; set; }


        // Relaciones
        public Pedidos Pedido { get; set; }
        public Productos Producto { get; set; }

    }
}
