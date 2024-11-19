namespace LojaCCDomain.Models
{
    public class ItemPedido
    {
        public required string ItemPedidoId { get; set; }
        public required string PedidoId { get; set; }
        public required string ItemId { get; set; }
        public required int Quantidade { get; set; }
        public required float Valor { get; set; }
    }
}
