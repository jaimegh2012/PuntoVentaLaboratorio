namespace POSApi.Controllers.Productos.Dtos
{
    public class ProductoDTO
    {
        public string? NombreProducto { get; set; }

        public decimal? Precio { get; set; }

        public int? CodCategoria { get; set; }

        public bool? Activo { get; set; }
    }
}
