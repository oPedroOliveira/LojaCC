namespace LojaCCDomain.Models
{
    public class Cliente
    {
        public required string ClienteId { get; set; }
        public IList<Pedido>? Pedidos { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string CPF { get; set; }
        public string? CEP { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? Logradouro { get; set; }
    }
}
