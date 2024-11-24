namespace LojaCCDomain.Models
{
    public class ItemPedido
    {
        public ItemPedido()
        {
            ItemPedidoId = Guid.NewGuid().ToString();
        }
        public string ItemPedidoId { get; set; }
        public string PedidoId { get; set; }
        public string ItemId { get; set; }
        public required int Quantidade { get; set; }
        public required float Valor { get; set; }
    }
}
