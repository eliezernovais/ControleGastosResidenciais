using Backend.Enums;

namespace Backend.DTOs.Transacao
{
    public class TransacaoRequest
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public required TipoTransacao Tipo { get; set; }
        public required int PessoaId { get; set; }
    }
}
