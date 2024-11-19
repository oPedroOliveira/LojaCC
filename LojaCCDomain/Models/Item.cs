namespace LojaCCDomain.Models
{
    public class Item
    {
        public required string ItemId { get; set; }
        public required int Quantidade { get; set; }
        public required float Preco { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Peso { get; set; }
        public IList<ItemPedido>? ItensPedido { get; set; }

    }
}
