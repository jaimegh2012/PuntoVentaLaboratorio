namespace POSApi.Controllers.Pedidos.Dtos
{
    public class PedidoDTO
    {
        public int? CodCliente { get; set; }

        public int? CodDireccion { get; set; }

        public int? CodUsuario { get; set; }

        public decimal? Total { get; set; }

        public bool? Activo { get; set; }

        public List<DetallePedidoDTO> DetallePedido { get; set; } = new List<DetallePedidoDTO>();
    }

    public class DetallePedidoDTO
    {
        public int? CodDetallePedido { get; set; }
        public int? CodProducto { get; set; }
        public int? CodPedido { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? Total { get; set; }

        public bool? Activo { get; set; }

    }
}
