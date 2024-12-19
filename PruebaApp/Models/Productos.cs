namespace PruebaApp.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relación con PedidoProductos
        public ICollection<PedidoProductos>? PedidoProductos { get; set; }
    }
}
