using Backend.Enums;

namespace Backend.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaID { get; set; }

        private Transacao() { }
        public Transacao(string descricao, decimal valor, TipoTransacao tipo, int pessoaId) {
            // Verifica se a Descrição é vazia
            if(string.IsNullOrWhiteSpace(descricao)) {
                throw new ArgumentException("A descrição nao deve ser vazia", nameof(descricao));
            }
            // Verifica se o Valor é menor ou igual á zero
            else if(valor <= 0){
                throw new ArgumentException("O valor deve ser maior que zero", nameof(valor));
            }else {
                Descricao = descricao;
                Valor = valor;
                Tipo = tipo;
                PessoaID = pessoaId;
            }
        }

    }
}
