namespace LojaCCDomain.Models
{
    public class Pedido
    {
        public required string ClienteId { get; set; }
        public required string PedidoId { get; set; }
        public required IList<ItemPedido> ItensPedido { get; set; }
        public required float ValorTotal { get; set; }
        public required float QtdTotal { get; set; }
        public string? Status { get; set; }
    }
}
