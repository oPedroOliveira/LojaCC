using System.Text.RegularExpressions;

namespace LojaCCDomain.Models
{
    public class Cliente
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid().ToString();
        }
        public string ClienteId { get; set; }
        public User? User { get; set; }
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
