using Backend.Enums;

namespace Backend.DTOs.Transacao
{
    public class TransacaoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaId { get; set; }
    }
}
